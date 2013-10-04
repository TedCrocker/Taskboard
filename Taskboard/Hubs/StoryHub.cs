using System.Linq;
using Microsoft.AspNet.SignalR;
using Taskboard.DataAccess;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class StoryHub : BaseHub<Story>
	{

		public StoryHub(IDataRepository<Story> storyRepo)
		{
			_repository = storyRepo;
		}

		public override void Add()
		{
			var story = new Story()
				{
					Id = ShortGuid.Get(),
					Left = 300,
					Top = 100,
					Content = "",
				};
			_repository.Add(story);
			Clients.All.add(story);
		}
	}
}