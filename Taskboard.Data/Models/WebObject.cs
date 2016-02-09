using Newtonsoft.Json;

namespace Taskboard.Data.Models
{
	public class WebObject : Entity
	{
		[JsonProperty("top")]
		public int Top { get; set; }
		[JsonProperty("left")]
		public int Left { get; set; }
		[JsonProperty("content")]
		public string Content { get; set; }
		[JsonProperty("workFlowState")]
		public WorkFlowState WorkFlowState { get; set; }
	}
}