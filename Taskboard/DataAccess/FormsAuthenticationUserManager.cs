using System;
using System.Web.Security;

namespace Taskboard.DataAccess
{
	public class DummyFormsAuthenticationUserManager : IUserManager
	{
		private static string _displayName;

		public string DisplayName
		{
			get { return _displayName; }
		}

		public bool Authenticate(string userName, string password)
		{
			if (password != "Password123TrapWire")
			{
				_displayName = null;
				return false;
			}

			FormsAuthentication.SetAuthCookie(userName, true);
			_displayName = userName;

			return true;
		}

		public void Unauthenticate()
		{
			FormsAuthentication.SignOut();
		}
	}
}