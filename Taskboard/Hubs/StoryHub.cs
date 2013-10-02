using System.Linq;
using Microsoft.AspNet.SignalR;
using Taskboard.DataAccess;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class StoryHub : Hub
	{
		private IDataRepository<Story> _storyRepo;

		public StoryHub(IDataRepository<Story> storyRepo)
		{
			_storyRepo = storyRepo;
		}

		public void AddStory()
		{
			var story = new Story()
				{
					Id = ShortGuid.Get(),
					Left = 300,
					Top = 100,
					Content = "",
				};
			_storyRepo.Add(story);
			Clients.All.addStory(story);
		}

		public void UpdateStory(Story story)
		{
			_storyRepo.Update(story);
			Clients.AllExcept(Context.ConnectionId).updateStory(story);
		}

		public void DeleteTask(Story story)
		{
			_storyRepo.Delete(story);
			Clients.All.deleteStory(story);
		}

		public void GetAll()
		{
			var stories = _storyRepo.GetWhere(s => true).ToArray();
			Clients.Caller.getAll(stories);
		}
	}
}