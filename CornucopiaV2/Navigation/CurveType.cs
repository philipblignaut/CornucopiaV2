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
		Curve = 0,
		Line = 1,
		Square = 2,
		Rose = 4,
		ZigZag = 3,
	}
}