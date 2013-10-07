using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskboard.DataAccess;

namespace Taskboard.Controllers
{
	public class AccountController : Controller
	{
		private IUserManager _manager;

		public AccountController(IUserManager manager)
		{
			_manager = manager;
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult Index(string userName, string password)
		{
			if (_manager.Authenticate(userName, password))
			{
				return RedirectToActionPermanent("Index", "Main");
				
			}

			return View();
		}
	}
}
