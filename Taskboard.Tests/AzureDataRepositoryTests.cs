using System.Linq;
using Microsoft.WindowsAzure.Storage;
using NUnit.Framework;
using Taskboard.DataAccess;
using Taskboard.Models;

namespace Taskboard.Tests
{
	public class AzureDataRepositoryTests
	{
		[Test]
		public void CanBuildRepository()
		{
			var repo = new AzureTableRepository<TaskItem, int>();
		}

		[Test]
		public void CanAddEntity()
		{
			var task = new TaskItem() { Id = 1 };
			var repo = new AzureTableRepository<TaskItem, int>();
			repo.Add(task);

		}

		[Test]
		public void AzureEntityGetValueTest()
		{
			var azureEntity = new TaskItem() { Id = 1, Content = "Content", Left = 2, Top = 3 };
			
			var props = azureEntity.WriteEntity(new OperationContext());
			Assert.That(props.Count(), Is.EqualTo(4));
		}
	}
}
