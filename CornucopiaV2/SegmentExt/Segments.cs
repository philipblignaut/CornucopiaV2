using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public class Segments
	{
		public SegmentsBetween SegmentsBetween
			(double start
			, double end
			, double segmentLength
			, bool isAngleDegrees = false
			)
		{
			SegmentsBetween result = new SegmentsBetween();
			List<Segment> segmentList = new List<Segment>();
			if (isAngleDegrees)
			{
				start = start.FixDegrees0T360();
				end = end.FixDegrees0T360();
				if (start > end)
				{
					double temp = start;
					start = end;
					end = temp;
				}
				if (end - start > 180)
				{
					double temp = start;
					start = end;
					end = temp + 360;
				}
			}
			double distance = (end - start).Abs();
			int segments = (int)(distance / segmentLength);
			for (int segmentIndex = 1; segmentIndex < segments; segmentIndex++)
			{
				segmentList
					.Add
						(new Segment
							(0
							, Math.Min(start, end)
								+ distance 
								* segmentIndex 
								/ segments
							)
						)
						;
			}
			segments += 2;
			segmentList.Add(new Segment(0, start));
			segmentList.Add(new Segment(0, end));
			segmentList
				.Sort
				((segment1, segment2) =>
					segment1.Value.CompareTo(segment2.Value)
				)
				;
			Segment[] segmentArray = segmentList.ToArray();
			for (int segmentIndex = 0; segmentIndex < segments / 2; segmentIndex++)
			{
				segmentArray[segmentIndex].Index = segmentIndex;
				segmentArray[segmentArray.Count() - segmentIndex - 1].Index = segmentIndex;
			}
			result.MaxSegmentIndex = segments / 2 - 1;
			result.SegmentLenght = distance / (segments - 2);
			result.Segments = segmentArray.ToList();
			return result;
		}

	}
}