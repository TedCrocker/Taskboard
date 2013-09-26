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
	}
}