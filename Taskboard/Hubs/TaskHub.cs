using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Taskboard.Models;

namespace Taskboard.Hubs
{

	public class TaskHub : Hub
	{
		private static readonly List<TaskItem> _tasks = new List<TaskItem>();
		

		public void AddTask(TaskItem task)
		{
			_tasks.Add(task);

			Clients.AllExcept(Context.ConnectionId).addTask(task);
		}
	}
}