using System;
using Taskboard.Data;
using Taskboard.Data.Azure;


namespace Taskboard.Models
{
	public class Log : AzureEntity
	{
		public DateTime TimeStamp { get; set; }
		public string Text { get; set; }
	}
}