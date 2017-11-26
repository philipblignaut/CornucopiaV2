using System;

namespace CornucopiaV2
{
	public static class CRandom
	{
		private static Random random = new Random(Environment.TickCount);

		public static int Next()
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

		public static float NextFloat()
		{
			return (float)random.NextDouble();
		}

		public static float NextFloat(float maxValue)
		{
			return (float)random.NextDouble() * maxValue;
		}

		public static float NextFloat(float minValue, float maxValue)
		{
			return (float)random.NextDouble() * (maxValue - minValue) + minValue;
		}

		public static double NextDouble()
		{
			return random.NextDouble();
		}

		public static double NextDouble(double maxValue)
		{
			return random.NextDouble() * maxValue;
		}

		public static double NextDouble(double minValue, double maxValue)
		{
			return random.NextDouble() * (maxValue - minValue) + minValue;
		}

		public static void NextBytes(byte[] buffer)
		{
			random.NextBytes(buffer);
		}

		public static void ReSeed(int seed)
		{
			random = new Random(Environment.TickCount | seed);
		}

	}
}