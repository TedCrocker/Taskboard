using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;
using Serilog;
using Taskboard.Data.Azure;
using Taskboard.Data.Models;
using CloudStorageAccount = Microsoft.WindowsAzure.CloudStorageAccount;

namespace Taskboard.Tests
{
	public class AzureDataRepositoryTests
	{
		private ILogger _logger;

		[SetUp]
		public void Setup()
		{
			var storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var client = storageAccount.CreateCloudTableClient();
			client.DeleteTableIfExist("Tasks");
			_logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
		}

		[Test]
		public void CanBuildRepository()
		{
			var repo = new AzureTableRepository<TaskItem>(_logger);
		}

		[Test]
		public void CanAddEntity()
		{
			var task = new TaskItem() { Id = "1" };
			var repo = new AzureTableRepository<TaskItem>(_logger);
			repo.Add(task);
		}

		[Test]
		public void CanGetEntity()
		{
			var task = new TaskItem() { Id = "1", Left = 2, Top = 3, Content = "Yar"};
			var repo = new AzureTableRepository<TaskItem>(_logger);
			repo.Add(task);

			var fetchedTask = repo.Get(task.Id);

			Assert.That(fetchedTask.Id, Is.EqualTo(task.Id));
			Assert.That(fetchedTask.Left, Is.EqualTo(task.Left));
			Assert.That(fetchedTask.Top, Is.EqualTo(task.Top));
			Assert.That(fetchedTask.Content, Is.EqualTo(task.Content));
		}

		[Test]
		public void AzureEntityGetValueTest()
		{
			var taskItem = new TaskItem() { Id = "1", Content = "Content", Left = 2, Top = 3 };
			
			//var props = taskItem.WriteEntity(new OperationContext());
			//Assert.That(props.Count(), Is.EqualTo(6));

			var newTask = new TaskItem();
			//newTask.ReadEntity(props, new OperationContext());

			Assert.That(newTask.Id, Is.EqualTo(taskItem.Id));
			Assert.That(newTask.Content, Is.EqualTo(taskItem.Content));
			Assert.That(newTask.Left, Is.EqualTo(taskItem.Left));
			Assert.That(newTask.Top, Is.EqualTo(taskItem.Top));
		}

		[Test]
		public void CanUpdateAzureEntityItem()
		{
			var taskItem = new TaskItem() {Id = "1", Left = 23};
			var repo = new AzureTableRepository<TaskItem>(_logger);
			repo.Add(taskItem);
			taskItem.Left = 46;
			repo.Update(taskItem);
			var fetched = repo.Get("1");

			Assert.That(fetched.Left, Is.EqualTo(taskItem.Left));
		}

		[Test]
		public void CanGetMultipleValuesOfTheSameType()
		{
			var repo = new AzureTableRepository<TaskItem>(_logger);
			repo.Add(new TaskItem(){ Id = "1"});
			repo.Add(new TaskItem(){Id = "2", Left = 4});
			repo.Add(new TaskItem(){Id = "3"});

			var fetched = repo.GetWhere(t => t.Left == 4);
			Assert.That(fetched.Count(), Is.EqualTo(1));
			Assert.That(fetched.First().Id, Is.EqualTo("2"));
		}
	}
}
