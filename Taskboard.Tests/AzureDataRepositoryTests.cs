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
			var repo = new DataRepository(new AzureTableDataSource());
		}

		[Test]
		public void CanAddEntity()
		{
			var task = new TaskItem(){ Id = 1};
			var repo = new DataRepository(new AzureTableDataSource());
			repo.Add(task);

		}
    }
}
