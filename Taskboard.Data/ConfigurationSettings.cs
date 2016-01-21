using System;
using System.Configuration;

namespace Taskboard.Data
{
	public static class ConfigurationSettings
	{
		private static string _connectionString = null;
		public static string ConnectionString
		{
			get
			{
				if (string.IsNullOrEmpty(_connectionString))
				{
					_connectionString = Environment.GetEnvironmentVariable("AzureStorage") ?? ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString;
				}

				return _connectionString;
			}
		}

		private static string _passwordToTestAgainst = null;
		public static string PasswordToTestAgainst
		{
			get
			{
				if (string.IsNullOrEmpty(_passwordToTestAgainst))
				{
					_passwordToTestAgainst = Environment.GetEnvironmentVariable("passwordToTestAgainst") ?? "Password123TrapWire";	
				}
				return _passwordToTestAgainst;
				
			}
		}

		private static string _pageTitle = null;
		public static string Title
		{
			get
			{
				if (_pageTitle == null)
				{
					_pageTitle = Environment.GetEnvironmentVariable("PageTitle") ?? "TrapWire";
				}

				return _pageTitle;
			}
		}
	}
}