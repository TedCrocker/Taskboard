using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Taskboard.DataAccess
{
	public class AzureTableRepository<T> : IDataRepository<T> where T : ITableEntity
	{
		private readonly CloudTable _table;
		private static readonly HashSet<T> _entities = new HashSet<T>();

		public AzureTableRepository(string connectionString = "UseDevelopmentStorage=true")
		{
			var storageAccount = CloudStorageAccount.Parse(connectionString);
			var client = storageAccount.CreateCloudTableClient();
			_table = client.GetTableReference("Tasks");
			_table.CreateIfNotExists();
		}

		public void Add(T entity)
		{
			_table.Execute(TableOperation.Insert(entity));
			_entities.Add(entity);
		}

		public void Delete(T entity)
		{
			var storedEntity = _entities.First(e => e.RowKey == entity.RowKey);
			_table.Execute(TableOperation.Delete(storedEntity));
			_entities.RemoveWhere(e => e.RowKey == entity.RowKey);
		}

		public T Get(string id)
		{
			var type = typeof (T);
			var result = _table.Execute(TableOperation.Retrieve<T>(type.Name, id));

			T returnValue = default(T);

			if (result.HttpStatusCode == 200)
			{
				returnValue = (T)Convert.ChangeType(result.Result, type);
			}

			return returnValue;
		}

		public void Update(T entity)
		{
			var storedEntity = _entities.First(e => e.RowKey == entity.RowKey);

			storedEntity.ReadEntity(entity.WriteEntity(new OperationContext()), new OperationContext());
			_table.Execute(TableOperation.Merge(storedEntity));
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
	}
}