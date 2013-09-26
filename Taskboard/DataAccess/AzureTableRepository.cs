using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Taskboard.DataAccess
{
	public class AzureTableRepository<T, K> : IDataRepository<T, K> where T : class
	{
		private CloudTable _table;

		public AzureTableRepository(string connectionString = "UseDevelopmentStorage=true")
		{
			var storageAccount = CloudStorageAccount.Parse(connectionString);
			var client = storageAccount.CreateCloudTableClient();
			_table = client.GetTableReference("Tasks");
			_table.CreateIfNotExists();
		}

		//public IEntity Add(int id, IEntity entity)
		//{
		//	dynamic item = new ElasticTableItem();
		//	item.Document = JsonConvert.SerializeObject(entity);
		//	item.EntityType = entity.GetType().Name;

		//	item.RowKey = item.ID.ToString();
		//	item.PartitionKey = "Yar";

		//	_table.Execute(TableOperation.Insert(item));
		//	return entity;
		//}

		//public IEntity Update(int id, IEntity entity)
		//{
		//	throw new System.NotImplementedException();
		//}

		//public IEntity Fetch(int id)
		//{
		//	throw new System.NotImplementedException();
		//}

		//public void Delete(int id)
		//{
		//	throw new System.NotImplementedException();
		//}
		public void Add(T entity)
		{
			//dynamic item =
		}

		public void Delete(T entity)
		{
			throw new NotImplementedException();
		}

		public T Get(K id)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetWhere(Expression<Func<T, bool>> whereCondition)
		{
			throw new NotImplementedException();
		}
	}
}