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
			var task = new TaskItem(){ Id = 1};
			var repo = new AzureTableRepository<TaskItem, int>();
			repo.Add(task);

		}
    }
}
