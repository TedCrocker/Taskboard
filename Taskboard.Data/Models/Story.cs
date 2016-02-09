using System;
using Newtonsoft.Json;

namespace Taskboard.Data.Models
{
	public class Story : WebObject
	{
		[JsonProperty("opened")]
		public DateTime? Opened { get; set; }
		[JsonProperty("closed")]
		public DateTime? Closed { get; set; }
		[JsonProperty("size")]
		public StorySize? Size { get; set; }
	}
}