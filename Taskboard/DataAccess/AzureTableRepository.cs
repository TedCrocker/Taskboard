using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog;

namespace Taskboard.DataAccess
{
	public class AzureTableRepository<T> : IDataRepository<T> where T : ITableEntity
	{
		private readonly CloudTable _table;
		private static readonly HashSet<T> _entities = new HashSet<T>();
		private bool _updatePending = false;
		private readonly Timer _timer;
		private readonly ISet<T> _entitiesWithPendingUpdates = new HashSet<T>();
		private readonly ILogger _log;

		public AzureTableRepository(ILogger log)
		{
			_log = log;
			var storageAccount = CloudStorageAccount.Parse(ConfigurationSettings.ConnectionString);
			var client = storageAccount.CreateCloudTableClient();
			_table = client.GetTableReference("Tasks");
			_table.CreateIfNotExists();

			_timer = new Timer(ExecuteUpdate, null, TimeSpan.Zero, TimeSpan.FromSeconds(7));
		} 

		public void Add(T entity)
		{
			lock (_lock)
			{
				_table.Execute(TableOperation.Insert(entity));
				_entities.Add(entity);
			}
		}

		public void Delete(T entity)
		{
			lock (_lock)
			{
				var storedEntity = _entities.First(e => e.RowKey == entity.RowKey);
				_table.Execute(TableOperation.Delete(storedEntity));
				_entities.RemoveWhere(e => e.RowKey == entity.RowKey);
			}
		}

		public void Clear()
		{
			try
			{
				var batch = new TableBatchOperation();
				foreach (var entity in _entities)
				{
					batch.Add(TableOperation.Delete(entity));
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
			T returnValue =  _entitiesWithPendingUpdates.FirstOrDefault(e => e.RowKey == id && e.PartitionKey == type.Name);

			if (returnValue == null)
			{
				var result = _table.Execute(TableOperation.Retrieve<T>(type.Name, id));
				if (result.HttpStatusCode == 200)
				{
					returnValue = (T)Convert.ChangeType(result.Result, type);
				}
			}

			return returnValue;
		}

		public void Update(T entity)
		{
			lock (_lock)
			{
				var storedEntity = _entities.First(e => e.RowKey == entity.RowKey);

				storedEntity.ReadEntity(entity.WriteEntity(new OperationContext()), new OperationContext());
				try
				{
					_entitiesWithPendingUpdates.Add(storedEntity);
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
						batchOperation.Add(TableOperation.Merge(entity));
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
					var item = Activator.CreateInstance<T>();

					item.ETag = etag;
					item.PartitionKey = pk;
					item.RowKey = rk;
					item.Timestamp = ts;
					item.ReadEntity(props, null);

					return item;
				};
			
			var queryResults = _table.ExecuteQuery(query, resolver, null, null).ToList<T>();

			foreach (var result in queryResults)
			{
				if (!_entities.Contains(result))
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