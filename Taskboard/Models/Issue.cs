using Newtonsoft.Json;

namespace Taskboard.Models
{
	public class Issue : WebObject
	{
		[JsonProperty("assignedTo")]
		public string AssignedTo { get; set; }
		[JsonProperty("color")]
		public string Color { get; set; }
		[JsonProperty("height")]
		public int Height { get; set; }
		[JsonProperty("width")]
		public int Width { get; set; }
	}
}