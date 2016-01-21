using System;
using Taskboard.Data;

namespace Taskboard.Models
{
	public class Log : AzureEntity
	{
		public DateTime TimeStamp { get; set; }
		public string Text { get; set; }
	}
}