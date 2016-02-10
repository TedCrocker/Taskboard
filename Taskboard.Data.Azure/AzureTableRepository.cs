using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog;
using Taskboard.Data.Models;

namespace Taskboard.Data.Azure
{
	public class AzureTableRepository<T> : IDataRepository<T> where T : Entity
	{
		private readonly CloudTable _table;
		private static readonly HashSet<T> _entities = new HashSet<T>();
		private bool _updatePending = false;
		private readonly Timer _timer;
		private readonly ISet<T> _entitiesWithPendingUpdates = new HashSet<T>();
		private readonly ILogger _log;
		private readonly Type _baseType;

		public AzureTableRepository(ILogger log)
		{
			_log = log;
			var storageAccount = CloudStorageAccount.Parse(ConfigurationSettings.ConnectionString);
			var client = storageAccount.CreateCloudTableClient();
			_table = client.GetTableReference("Tasks");
			_table.CreateIfNotExists();
			_baseType = typeof (T);

			_timer = new Timer(ExecuteUpdate, null, TimeSpan.Zero, TimeSpan.FromSeconds(7));
		} 

		public void Add(T entity)
		{
			lock (_lock)
			{
				var dynamicTableEntity = entity.ConvertToDynamicTableEntity<T>();
				_table.Execute(TableOperation.Insert(dynamicTableEntity));
				_entities.Add(entity);
			}
		}

		public void Delete(T entity)
		{
			lock (_lock)
			{
				var storedEntity = _entities.First(e => e.Id == entity.Id);
				_table.Execute(TableOperation.Delete(storedEntity.ConvertToDynamicTableEntity<T>()));
				_entities.RemoveWhere(e => e.Id == entity.Id);
			}
		}

		public void Clear()
		{
			try
			{
				var batch = new TableBatchOperation();
				foreach (var entity in _entities)
				{
					batch.Add(TableOperation.Delete(entity.ConvertToDynamicTableEntity<T>()));
				}
				_table.ExecuteBatch(batch);
				_entities.Clear();
			}
			catch (Exception e)
			{
				_log.Error(e, "AzureTableRepository.Clear");
			}
		}

		public T Get(string id)
		{
			var type = typeof (T);
			T returnValue = _entitiesWithPendingUpdates.FirstOrDefault(e => e.Id == id);
			
			if (returnValue == null)
			{
				var result = _table.Execute(TableOperation.Retrieve<DynamicTableEntity>(type.Name, id));
				if (result.HttpStatusCode == 200)
				{
					var tableEntity = (DynamicTableEntity) Convert.ChangeType(result.Result, typeof (DynamicTableEntity));
					returnValue = tableEntity.ConvertToEntity<T>();
				}
			}
			
			return returnValue;
		}

		public void Update(T entity)
		{
			lock (_lock)
			{
				if (_entities.Any(e => e.Id == entity.Id))
				{
					var storedEntity = _entities.First(e => e.Id == entity.Id);
					_entities.Remove(storedEntity);
					_entities.Add(entity);
				}
				
				try
				{
					_entitiesWithPendingUpdates.Add(entity);
					_updatePending = true;
				}
				catch (Exception e)
				{
					_log.Error(e, "AzureTableRepository.Update");
				}
			}
		}

		private static readonly object _lock = new object();
		private void ExecuteUpdate(object obj)
		{
			if (_updatePending)
			{
				lock (_lock)
				{
					var batchOperation = new TableBatchOperation();
					foreach (var entity in _entitiesWithPendingUpdates)
					{
						batchOperation.Add(TableOperation.Merge(entity.ConvertToDynamicTableEntity<T>()));
					}

					try
					{
						_table.ExecuteBatch(batchOperation);
						_entitiesWithPendingUpdates.Clear();
					}
					catch (Exception e)
					{
						_log.Error(e, "AzureTableRepository.ExecuteUpdate");
					}
				}
			}
			_updatePending = false;
		}

		public IList<T> GetWhere(Func<T, bool> whereCondition)
		{
			var query = new TableQuery().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, typeof (T).Name));
			EntityResolver<T> resolver = (pk, rk, ts, props, etag) =>
			{
				var tableEntity = new DynamicTableEntity(pk, rk, etag, props);
				return tableEntity.ConvertToEntity<T>();
			};
			
			var queryResults = _table.ExecuteQuery(query, resolver, null, null).ToList<T>();

			foreach (var result in queryResults)
			{
				var haveLoadedEntity = _entities.Any(entity => entity.Id == result.Id);
				if (!haveLoadedEntity)
				{
					_entities.Add(result);
				}
			}

			return queryResults.Where(whereCondition).ToList();
		}

		public void Dispose()
		{
			_timer.Dispose();
		}
	}
}