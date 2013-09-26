﻿using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.StorageClient;
using NUnit.Framework;
using Taskboard.DataAccess;
using Taskboard.Models;
using CloudStorageAccount = Microsoft.WindowsAzure.CloudStorageAccount;

namespace Taskboard.Tests
{
	public class AzureDataRepositoryTests
	{
		[SetUp]
		public void Setup()
		{
			var storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var client = storageAccount.CreateCloudTableClient();
			client.DeleteTableIfExist("Tasks");
		}

		[Test]
		public void CanBuildRepository()
		{
			var repo = new AzureTableRepository<TaskItem>();
		}

		[Test]
		public void CanAddEntity()
		{
			var task = new TaskItem() { Id = 1 };
			var repo = new AzureTableRepository<TaskItem>();
			repo.Add(task);
		}

		[Test]
		public void CanGetEntity()
		{
			var task = new TaskItem() { Id = 1, Left = 2, Top = 3, Content = "Yar"};
			var repo = new AzureTableRepository<TaskItem>();
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
			var taskItem = new TaskItem() { Id = 1, Content = "Content", Left = 2, Top = 3 };
			
			var props = taskItem.WriteEntity(new OperationContext());
			Assert.That(props.Count(), Is.EqualTo(4));

			var newTask = new TaskItem();
			newTask.ReadEntity(props, new OperationContext());

			Assert.That(newTask.Id, Is.EqualTo(taskItem.Id));
			Assert.That(newTask.Content, Is.EqualTo(taskItem.Content));
			Assert.That(newTask.Left, Is.EqualTo(taskItem.Left));
			Assert.That(newTask.Top, Is.EqualTo(taskItem.Top));
		}
	}
}
