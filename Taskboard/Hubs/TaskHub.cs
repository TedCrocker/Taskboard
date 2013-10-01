using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class TaskHub : Hub
	{
		private static int _idCounter = 0;

		public void AddTask()
		{
			var task = new TaskItem()
				{
					Id = _idCounter++,
					Left = 100,
					Top= 100,
					Content = "New Task"
				};
			Clients.All.addTask(task);
		}

		public void UpdateTask(TaskItem task)
		{
			Clients.All.updateTask(task);
		}

		public void DeleteTask(TaskItem task)
		{
			Clients.All.deleteTask(task);
		}
	}
}