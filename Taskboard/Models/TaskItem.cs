using Newtonsoft.Json;
using Taskboard.DataAccess;

namespace Taskboard.Models
{
	public class TaskItem : AzureEntity
	{
		[JsonProperty("top")]
		public int Top { get; set; }
		[JsonProperty("left")]
		public int Left { get; set; }
		[JsonProperty("content")]
		public string Content { get; set; }
		[JsonProperty("assignedTo")]
		public string AssignedTo { get; set; }
		[JsonProperty("workFlowState")]
		public TaskState WorkFlowState { get; set; }
	}

	public enum TaskState
	{
		Pending,
		Open,
		Closed
	}
}