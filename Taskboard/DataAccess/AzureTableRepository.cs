using System;
using System.Collections.Generic;
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

		public IList<T> GetWhere(Expression<Func<T, bool>> whereCondition)
		{
			throw new NotImplementedException();
		}
	}
}