using Taskboard.Data;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class TaskHub : BaseHub<TaskItem>
	{
		public TaskHub(IDataRepository<TaskItem> taskRepo)
		{
			_repository = taskRepo;
		}

		public override void Add()
		{
			var task = new TaskItem()
				{
					Id = ShortGuid.Get(),
					Left = 100,
					Top= 100,
					Content = "New Task"
				};
			_repository.Add(task);
			Clients.All.add(task);
		}
	}
}