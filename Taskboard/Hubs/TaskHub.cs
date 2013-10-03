using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Taskboard.DataAccess;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class TaskHub : Hub
	{
		private IDataRepository<TaskItem> _taskRepo;
		public TaskHub(IDataRepository<TaskItem> taskRepo)
		{
			_taskRepo = taskRepo;
		}

		public void Add()
		{
			var task = new TaskItem()
				{
					Id = ShortGuid.Get(),
					Left = 100,
					Top= 100,
					Content = "New Task"
				};
			_taskRepo.Add(task);
			Clients.All.add(task);
		}

		public void Update(TaskItem task)
		{
			_taskRepo.Update(task);
			Clients.AllExcept(Context.ConnectionId).update(task);
		}

		public void Remove(TaskItem task)
		{
			_taskRepo.Delete(task);
			Clients.All.remove(task);
		}

		public void GetAll()
		{
			var tasks = _taskRepo.GetWhere(t => true).ToArray();
			Clients.Caller.getAll(tasks);
		}
	}
}