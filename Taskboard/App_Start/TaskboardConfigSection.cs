using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Taskboard
{
	public class TaskboardConfigSection : ConfigurationSection
	{
		private static TaskboardConfigSection _instance = null;
		public static TaskboardConfigSection Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (TaskboardConfigSection) WebConfigurationManager.GetSection("taskboard");
				}
				return _instance;
			}
		}

		[ConfigurationProperty("requireAuthentication", DefaultValue = false, IsRequired = false)]
		public bool RequireAuthentication
		{
			get { return (bool) base["requireAuthentication"]; }
			set { base["requireAuthentication"] = value; }
		}
	}
}