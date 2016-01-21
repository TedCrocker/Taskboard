using System;
using System.Linq;
using Taskboard.Data;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class LogHub : BaseHub<Log>
	{
		public LogHub(IDataRepository<Log> logItemRepo)
		{
			_repository = logItemRepo;
		}

		public override void Add()
		{
			
		}

		public override void GetAll()
		{
			var entities = _repository.GetWhere(t => true).OrderBy(t => t.TimeStamp).ToArray();
			Clients.Caller.getAll(entities);
		}

		public void Add(string logText)
		{
			var logItem = new Log()
			{
				Id = ShortGuid.Get(),
				Text = logText,
				TimeStamp = DateTime.Now
			};

			_repository.Add(logItem);
			Clients.All.add(logItem);
		}

		public void ClearAll()
		{
			_repository.Clear();
			Clients.All.clearAll();
		}
	}
}