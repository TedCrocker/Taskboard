using Taskboard.DataAccess;
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
			var issue = new Issue()
				{
					Id = ShortGuid.Get(),
					Left = 400,
					Top = 300,
					Content = "Issue",
				};

			_repository.Add(issue);
			Clients.All.add(issue);
		}
	}
}