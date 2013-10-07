using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Ninject;
using Taskboard.DataAccess;

namespace Taskboard
{
	public static class DependencyConfig
	{
		public static void SetupDependencies()
		{
			var kernel = new StandardKernel();
			var resolver = new NinjectSignalRDependencyResolver(kernel);

			kernel.Bind(typeof (IDataRepository<>)).To(typeof (AzureTableRepository<>));
			kernel.Bind(typeof (IUserManager)).To(typeof (DummyFormsAuthenticationUserManager));

			RouteTable.Routes.MapHubs(new HubConfiguration()
				{
					Resolver = resolver
				});
		}
	}
}