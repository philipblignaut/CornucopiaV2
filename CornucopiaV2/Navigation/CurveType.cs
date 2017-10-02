using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	[FlagsAttribute]
	public enum CurveType
	{
		Curve = 1,
		Line = 2,
		Square = 4,
		Rose = 8,
		ZigZag = 16,
	}
}