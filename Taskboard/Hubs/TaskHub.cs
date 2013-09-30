using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class TaskHub : Hub
	{
		public void AddTask()
		{
			var task = new TaskItem()
				{
					Left = 100,
					Top= 100,
					Content = "New Task"
				};
			Clients.All.addTask(task);
		}
	}
}