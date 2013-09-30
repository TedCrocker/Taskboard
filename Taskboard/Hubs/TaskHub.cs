using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Taskboard.Models;

namespace Taskboard.Hubs
{

	public class TaskHub : Hub
	{
		public void AddTask()
		{
			var task = new TaskItem();
			Clients.All.addTask(task);
		}
	}
}