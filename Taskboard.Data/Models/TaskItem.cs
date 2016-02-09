using Newtonsoft.Json;

namespace Taskboard.Data.Models
{
	public class TaskItem : WebObject
	{
		[JsonProperty("assignedTo")]
		public string AssignedTo { get; set; }
	}
}