using System;
using System.Net;
using System.Web;
using System.Web.Security;

namespace Taskboard.DataAccess
{
	public class DummyFormsAuthenticationUserManager : IUserManager
	{
		public string DisplayName
		{
			get
			{
				var httpCookie = HttpContext.Current.Request.Cookies["UserName"];
				if (httpCookie != null)
				{
					return httpCookie.Value;
				}
				
				return "";
			}
		}

		public bool Authenticate(string userName, string password)
		{
			if (password != "Password123TrapWire")
			{
				if (HttpContext.Current.Request.Cookies["UserName"] != null)
				{
					HttpContext.Current.Response.Cookies.Remove("UserName");
				}
				return false;
			}

			FormsAuthentication.SetAuthCookie(userName, true);

			var cookie = new HttpCookie("UserName");
			cookie.Value = userName;
			HttpContext.Current.Response.Cookies.Add(cookie);

			return true;
		}

		public void Unauthenticate()
		{
			FormsAuthentication.SignOut();
		}
	}
}