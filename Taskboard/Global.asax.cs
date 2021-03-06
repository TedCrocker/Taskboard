﻿using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Taskboard.Data;

namespace Taskboard
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public IUserManager UserManager { get; set; }

		protected void Application_Start()
		{
			var config = TaskboardConfigSection.Instance;
			AreaRegistration.RegisterAllAreas();
			GlobalHost.HubPipeline.RequireAuthentication();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			var container = DependencyConfig.Initialize();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);


			UserManager = container.GetInstance<IUserManager>();
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			if (User != null && User.Identity != null && User.Identity.IsAuthenticated && string.IsNullOrEmpty(UserManager.DisplayName))
			{
				UserManager.Unauthenticate();
				Response.RedirectToRoute(new { controller = "Account", action = "Index"});
			}
		}
	}
}