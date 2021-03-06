﻿using System.Web;
using System.Web.Security;

namespace Taskboard.Data
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
			var passwordToTestAgainst = ConfigurationSettings.PasswordToTestAgainst;

			if (password != passwordToTestAgainst)
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