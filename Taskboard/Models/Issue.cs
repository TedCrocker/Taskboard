using Newtonsoft.Json;

namespace Taskboard.Models
{
	public class Issue : WebObject
	{
		[JsonProperty("assignedTo")]
		public string AssignedTo { get; set; }
		[JsonProperty("color")]
		public string Color { get; set; }
	}
}