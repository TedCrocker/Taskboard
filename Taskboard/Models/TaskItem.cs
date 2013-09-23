using Newtonsoft.Json;
using Taskboard.DataAccess;

namespace Taskboard.Models
{
	public class TaskItem : IEntity
	{
		[JsonProperty("top")]
		public int Top { get; set; }
		[JsonProperty("left")]
		public int Left { get; set; }
		[JsonProperty("content")]
		public string Content { get; set; }

		public int Id { get; set; }
	}
}