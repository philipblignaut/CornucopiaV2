using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public struct SegmentsBetween
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

		public override bool Equals(object obj)
		{
			SegmentsBetween other = new SegmentsBetween();
			if (obj is SegmentsBetween)
			{
				other = (SegmentsBetween)obj;
				return
					Segments.Count == other.Segments.Count
					&& SegmentLenght == other.SegmentLenght
					&& MaxSegmentIndex == other.MaxSegmentIndex
					;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return
				Segments.Count.GetHashCode()
				| SegmentLenght.GetHashCode()
				| MaxSegmentIndex.GetHashCode()
				;
		}

		public static bool operator ==(SegmentsBetween left, SegmentsBetween right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(SegmentsBetween left, SegmentsBetween right)
		{
			return !(left == right);
		}
	}

}