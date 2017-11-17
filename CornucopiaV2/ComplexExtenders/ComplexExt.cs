using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public static class ComplexExt
	{
		public static Complex Clone
			(this Complex complex
			) =>
			new Complex
			(complex.Real
			, complex.Imaginary
			)
			;

		public static Complex Pow
			(this Complex complex
			, double power
			) =>
			Complex.Pow(complex, power);

		public static Complex Sin
			(this Complex complex
			) =>
			Complex.Sin(complex);

		public static Complex Tan
			(this Complex complex
			) =>
			Complex.Tan(complex);

		public static string ToFormatString
			(this Complex complex
			)
		{
			double r = Math.Round(complex.Real, 2);
			double i = Math.Round(complex.Imaginary, 2);
			return $@"{{{r,9:##0.00},{i,9:##0.00}}}";
		}

	}
}