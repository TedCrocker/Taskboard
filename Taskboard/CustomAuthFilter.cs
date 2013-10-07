using System.Web.Mvc;

namespace Taskboard
{
	public class CustomAuthFilter : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
		{
			if (!TaskboardConfigSection.Instance.RequireAuthentication)
			{
				return true;
			}
			
			return base.AuthorizeCore(httpContext);
		}
	}
}