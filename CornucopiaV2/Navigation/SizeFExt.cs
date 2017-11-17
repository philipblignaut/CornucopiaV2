using System;
using System.Drawing;

namespace CornucopiaV2
{
	public static class SizeFExt
	{

		public static string ToFormatString
			(this SizeF size
			)
		{
			double width = Math.Round(size.Width, 2);
			double height = Math.Round(size.Height, 2);
			return $@"{{W={width,9:##0.00}}} {{H={height,9:##0.00}}}";
		}

	}
}