using System;

namespace Taskboard.Data
{
	public static class ShortGuid
	{
		public static string Get()
		{
			var guid = Guid.NewGuid();
			var enc = Convert.ToBase64String(guid.ToByteArray());
			enc = enc.Replace("/", "_").Replace("+", "-");
			return enc.Substring(0, 22);
		}
	}
}