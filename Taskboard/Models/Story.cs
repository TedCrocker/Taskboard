using System;
using Newtonsoft.Json;
using Taskboard.DataAccess;

namespace Taskboard.Models
{
	public class Story : AzureEntity
	{
		[JsonProperty("top")]
		public int Top { get; set; }
		[JsonProperty("left")]
		public int Left { get; set; }
		[JsonProperty("content")]
		public string Content { get; set; }
		[JsonProperty("workFlowState")]
		public WorkFlowState WorkFlowState { get; set; }
		[JsonProperty("opened")]
		public DateTime? Opened { get; set; }
		[JsonProperty("closed")]
		public DateTime? Closed { get; set; }
		[JsonProperty("size")]
		public StorySize? Size { get; set; }
	}
}