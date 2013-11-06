using Newtonsoft.Json;

namespace Taskboard.Models
{
	public class Issue : WebObject
	{
		[JsonProperty("assignedTo")]
		public string AssignedTo { get; set; }
	}
}