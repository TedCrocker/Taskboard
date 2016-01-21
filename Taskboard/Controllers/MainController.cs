using System.Web.Mvc;
using Taskboard.DataAccess;

namespace Taskboard.Controllers
{
	public class MainController : Controller
	{
		private IUserManager _userManager;

		public MainController(IUserManager userManager)
		{
			_userManager = userManager;
		}

		public ActionResult Index()
		{
			ViewBag.DisplayName = _userManager.DisplayName;
			return View();
		}
	}
}
