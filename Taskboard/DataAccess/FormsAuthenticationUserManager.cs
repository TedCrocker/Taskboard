using System;
using System.Web.Security;

namespace Taskboard.DataAccess
{
	public class DummyFormsAuthenticationUserManager : IUserManager
	{

		public bool Authenticate(string userName, string password)
		{
			if (password == "fail")
			{
				return false;
			}

			FormsAuthentication.SetAuthCookie(userName, true);

			return true;
		}

		public void Unauthenticate()
		{
			FormsAuthentication.SignOut();
		}
	}
}