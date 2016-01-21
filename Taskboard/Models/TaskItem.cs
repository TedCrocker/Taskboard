using Newtonsoft.Json;

namespace Taskboard.Models
{
	public class TaskItem : WebObject
	{
		[JsonProperty("assignedTo")]
		public string AssignedTo { get; set; }
	}
}