using System.Web.Mvc;
using Taskboard.Data;

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

		public ActionResult Future()
		{
			ViewBag.DisplayName = _userManager.DisplayName;
			return View();
		}
	}
}
