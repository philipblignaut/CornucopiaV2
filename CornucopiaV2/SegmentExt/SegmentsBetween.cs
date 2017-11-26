using System;
using System.Collections.Generic;

namespace CornucopiaV2
{
	public struct SegmentsBetween
		: IEquatable<SegmentsBetween>
	{
		public int MaxSegmentIndex { get; internal set; }
		public double SegmentLenght { get; internal set; }
		public List<Segment> Segments { get; internal set; }
		/// <summary>Record Constructor</summary>
		/// <param name="maxSegmentIndex">aaa<see cref="MaxSegmentIndex"/></param>
		/// <param name="segments">bbb<see cref="Segments"/></param>
		public SegmentsBetween
			(int maxSegmentIndex
			, List<Segment> segments
			)
		{
			MaxSegmentIndex = maxSegmentIndex;
			Segments = segments;
			SegmentLenght = 0;
		}

		public bool Equals(SegmentsBetween other)
			=> Segments.Count == other.Segments.Count
				&& SegmentLenght == other.SegmentLenght
				&& MaxSegmentIndex == other.MaxSegmentIndex
				;

		public override bool Equals(object obj)
			=> obj is SegmentsBetween other
				&& Equals(other)
				;

		public static bool operator ==
			(SegmentsBetween left
			, SegmentsBetween right
			) => left.Equals(right);

		public static bool operator !=
			(SegmentsBetween left
			, SegmentsBetween right
			) => !left.Equals(right);

		public override int GetHashCode()
			=> Segments.Count.GetHashCode()
			& SegmentLenght.GetHashCode()
			& MaxSegmentIndex.GetHashCode()
			;

		public override string ToString()
			=>
			$"{{{Segments.Count},{SegmentLenght},{MaxSegmentIndex}}}"
			;

	}

}