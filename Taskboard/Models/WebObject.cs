using Newtonsoft.Json;
using Taskboard.Data;
using Taskboard.Data.Azure;

namespace Taskboard.Models
{
	public class WebObject : AzureEntity
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