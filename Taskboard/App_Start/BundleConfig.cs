using System.Web;
using System.Web.Optimization;

namespace Taskboard
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/thirdparty/jquery.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/thirdparty/jquery-ui.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/thirdparty/modernizr.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
						"~/Content/thirdparty/jquery-ui/core.css",
						"~/Content/thirdparty/jquery-ui/resizable.css",
						"~/Content/thirdparty/jquery-ui/selectable.css",
						"~/Content/thirdparty/jquery-ui/accordion.css",
						"~/Content/thirdparty/jquery-ui/autocomplete.css",
						"~/Content/thirdparty/jquery-ui/button.css",
						"~/Content/thirdparty/jquery-ui/dialog.css",
						"~/Content/thirdparty/jquery-ui/slider.css",
						"~/Content/thirdparty/jquery-ui/tabs.css",
						"~/Content/thirdparty/jquery-ui/datepicker.css",
						"~/Content/thirdparty/jquery-ui/progressbar.css",
						"~/Content/thirdparty/jquery-ui/theme.css"));
		}
	}
}