using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Taskboard.DataAccess
{
	public class AzureTableDataSource : IDataSource
	{
		private CloudTable _table;

		public AzureTableDataSource(string connectionString = "UseDevelopmentStorage=true")
		{
			var storageAccount = CloudStorageAccount.Parse(connectionString);
			var client = storageAccount.CreateCloudTableClient();
			_table = client.GetTableReference("Tasks");
			_table.CreateIfNotExists();
		}

		public IEntity Add(int id, IEntity entity)
		{
			dynamic item = new ElasticTableItem();
			item.ID = id;
			item.Document = JsonConvert.SerializeObject(entity);
			item.EntityType = entity.GetType().Name;

			_table.Execute(TableOperation.Insert(item));
			return entity;
		}

		public IEntity Update(int id, IEntity entity)
		{
			throw new System.NotImplementedException();
		}

		public IEntity Fetch(int id)
		{
			throw new System.NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}