namespace CornucopiaV2
{
	public class NumberRangeTranslator
	{

		// NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin

			/// <summary>
			/// cfv iuoigouyg yguiouu iuyoiuypo
			/// </summary>
		public double OldMin { get; private set; }
		public double OldMax { get; private set; }
		public double NewMin { get; private set; }
		public double NewMax { get; private set; }
		/// <summary>Record Constructor</summary>
		/// <param name="oldMin">xxxxxx cfv iuoigouyg yguiouu iuyoiuypo<see cref="OldMin"/></param>
		/// <param name="oldMax"><see cref="OldMax"/></param>
		/// <param name="newMin"><see cref="NewMin"/></param>
		/// <param name="newMax"><see cref="NewMax"/></param>
		public NumberRangeTranslator
			(double oldMin
			, double oldMax
			, double newMin
			, double newMax
			)
		{
			OldMin = oldMin;
			OldMax = oldMax;
			NewMin = newMin;
			NewMax = newMax;
		}

		public double NewValue
			(double oldValue
			) => 
			(oldValue - OldMin)
			* (NewMax-NewMin) 
			/ (OldMax-OldMin)
			+ NewMin;

	}
}