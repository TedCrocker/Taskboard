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
		private CloudTable _table;

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
		}

		public void Delete(T entity)
		{
			_table.Execute(TableOperation.Delete(entity));
		}

		public T Get(int id)
		{
			var type = typeof (T);
			var result = _table.Execute(TableOperation.Retrieve<T>(type.Name, id.ToString()));

			T returnValue = default(T);

			if (result.HttpStatusCode == 200)
			{
				returnValue = (T)Convert.ChangeType(result.Result, type);
			}

			return returnValue;
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
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
			return queryResults.Where(whereCondition).ToList();
		}
	}
}