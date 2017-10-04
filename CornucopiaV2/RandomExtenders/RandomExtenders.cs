using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
    public static class CRandom
    {
		private static Random random = new Random(Environment.TickCount);

		public static  int Next()
		{
			return random.Next();
		}

		public static int Next(int maxValue)
		{
			return random.Next(maxValue);
		}

		public static int Next(int minValue, int maxValue)
		{
			return random.Next(minValue, maxValue);
		}

		public static double NextDouble()
		{
			return random.NextDouble();
		}

		public static void NextBytes(byte[] buffer)
		{
			random.NextBytes(buffer);
		}

	}
}