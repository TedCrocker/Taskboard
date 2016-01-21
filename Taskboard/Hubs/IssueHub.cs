using Taskboard.Data;
using Taskboard.Data.Azure;
using Taskboard.Models;

namespace Taskboard.Hubs
{
	public class IssueHub : BaseHub<Issue>
	{
		public IssueHub(IDataRepository<Issue> issueRepo)
		{
			_repository = issueRepo;
		}

		public override void Add()
		{
			Add("red");
		}

		public void Add(string color)
		{
			var issue = new Issue()
			{
				Id = ShortGuid.Get(),
				Left = 400,
				Top = 300,
				Content = "Issue",
				Color = color,
				Width = 300,
				Height = 70
			};

			_repository.Add(issue);
			Clients.All.add(issue);
		}
	}
}