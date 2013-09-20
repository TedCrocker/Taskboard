using Newtonsoft.Json;

namespace Taskboard.Models
{
	public class TaskItem
	{
		[JsonProperty("top")]
		public int Top { get; set; }
		[JsonProperty("left")]
		public int Left { get; set; }
		[JsonProperty("content")]
		public string Content { get; set; }
	}
}