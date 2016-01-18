using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using Taskboard.DataAccess;

namespace Taskboard
{
	public static class DependencyConfig
	{
		public static Container Initialize()
		{
			var container = new Container();

			InitializeContainer(container);

			var activator = new SimpleInjectorHubActivator(container);
			GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => activator);
			container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

			container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

			return container;
		}

		private static void InitializeContainer(Container container)
		{
			container.Register(typeof(IDataRepository<>), typeof(AzureTableRepository<>));
			container.Register<IUserManager, DummyFormsAuthenticationUserManager>();
		}
	}
}