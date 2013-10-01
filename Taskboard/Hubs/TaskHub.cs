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
		public TaskHub(IDataRepository<TaskItem> taskRepo)
		{
			_taskRepo = taskRepo;
		}

		private IDataRepository<TaskItem> _taskRepo;

		public void AddTask()
		{
			var task = new TaskItem()
				{
					Id = ShortGuid.Get(),
					Left = 100,
					Top= 100,
					Content = "New Task"
				};
			_taskRepo.Add(task);
			Clients.All.addTask(task);
		}

		public void UpdateTask(TaskItem task)
		{
			_taskRepo.Update(task);
			Clients.All.updateTask(task);
		}

		public void DeleteTask(TaskItem task)
		{
			_taskRepo.Delete(task);
			Clients.All.deleteTask(task);
		}

		public void GetAll()
		{
			var tasks = _taskRepo.GetWhere(t => true).ToArray();
			Clients.All.getAll(tasks);
		}
	}
}