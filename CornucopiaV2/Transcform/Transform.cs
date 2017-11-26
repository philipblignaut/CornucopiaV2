using System;

namespace CornucopiaV2
{
	public struct XTransform
		: IEquatable<XTransform>
	{

		public float FromMin { get; private set; }
		public float FromMax { get; private set; }
		public float ToMin { get; private set; }
		public float ToMax { get; private set; }

		/// <summary>Constructor</summary>
		/// <param name="fromMin"><see cref="FromMin"/></param>
		/// <param name="fromMax"><see cref="FromMax"/></param>
		/// <param name="toMin"><see cref="ToMin"/></param>
		/// <param name="toMax"><see cref="ToMax"/></param>
		public XTransform(float fromMin, float fromMax, float toMin, float toMax)
		{
			FromMin = fromMin;
			FromMax = fromMax;
			ToMin = toMin;
			ToMax = toMax;
		}

		public float ToValue
			(float value
			)
			=>	value/(FromMax-FromMin)
			;

		public bool Equals(XTransform other)
			=>
				FromMin == other.FromMin
				&& FromMax == other.FromMax
				&& ToMin == other.ToMin
				&& ToMax == other.ToMax
				;

		public override bool Equals(object obj)
			=>
			obj != null
			&& obj is XTransform other
			&& Equals(other)
			;

		public static bool operator ==(XTransform left, XTransform right)
			=> left.Equals(right)
			;

		public static bool operator !=(XTransform left, XTransform right)
			=> !left.Equals(right)
			;

		public override int GetHashCode()
			=>
			FromMin.GetHashCode()
			& FromMax.GetHashCode()
			& ToMin.GetHashCode()
			& ToMax.GetHashCode()
			;

		public override string ToString()
			=>
			$"{{{FromMin} {FromMax}}}"
			+ $" {{{ToMin} {ToMax}}}"
			;

	}

}