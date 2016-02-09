using System;

namespace Taskboard.Data.Models
{
	public class Log : Entity
	{
		public DateTime TimeStamp { get; set; }
		public string Text { get; set; }
	}
}