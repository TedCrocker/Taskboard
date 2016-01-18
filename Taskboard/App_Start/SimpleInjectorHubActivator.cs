using Microsoft.AspNet.SignalR.Hubs;
using SimpleInjector;

namespace Taskboard
{
	public class SimpleInjectorHubActivator : IHubActivator
	{
		private Container _container;

		public SimpleInjectorHubActivator(Container container)
		{
			_container = container;
		}

		public IHub Create(HubDescriptor descriptor)
		{
			return (IHub) _container.GetInstance(descriptor.HubType);
		}
	}
}