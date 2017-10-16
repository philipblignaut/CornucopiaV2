

public static PointF[] Intersection(PointF a1, PointF a2, PointF b1, PointF b2)
where (a1,a2) is one line segment and (b1,b2) is another.

This function would need to cover all the weird cases that most implementations or explanations gloss over. In order to account for the weirdness of coincident lines, the function could return an array of PointF's:

zero result points (or null) if the lines are parallel or do not intersect (infinite lines intersect but line segments are disjoint, or lines are parallel)
one result point (containing the intersection location) if they do intersect or if they are coincident at one point
two result points (for the overlapping part of the line segments) if the two lines are coincident
c# .net graphics geometry gdi+
shareimprove this question
edited May 23 at 12:34
community wiki
5 revs
Jared Updike
  	 	
I realize that this question is just asked so you could post your answer. You should mark it as the accepted answer. It wouldn't hurt to use less confrontational language in the question as well, FWIW. – tfinniga Feb 13 '10 at 1:59
  	 	
@tfinniga: I didn't realize it was confrontational until I rewrote it and made it sound like a puzzle instead of a demand. My goal wasn't to make other people do the work for me, but rather to prove that no other implementation even worked. (If you can prove me wrong and find a really good solution (that's on SO right now) that works flawlessly I would gladly give you 100 rep). – Jared Updike Feb 13 '10 at 2:38
  	 	
Thanks, I think that's much better. A bullet-proof implementation for this common need is valuable, and the rephrased question is much more pleasant. – tfinniga Feb 13 '10 at 4:42
add a comment
4 Answers
active oldest votes
up vote
7
down vote
accepted
Sounds like you have your solution, which is great. I have some suggestions for improving it.

The method has a major usability problem, in that it is very confusing to understand (1) what the parameters going in mean, and (2) what the results coming out mean. Both are little puzzles that you have to figure out if you want to use the method.

I would be more inclined to use the type system to make it much more clear what this method does.

I'd start by defining a type -- perhaps a struct, particularly if it was going to be immutable -- called LineSegment. A LineSegment consists of two PointF structs representing the end point.

Second, I would define an abstract base type "Locus" and derived types EmptyLocus, PointLocus, LineSegmentLocus and perhaps UnionLocus if you need to represent the locus that is the union of two or more loci. An empty locus is just a singleton, a point locus is just a single point, and so on.

Now your method signature becomes much more clear:

static Locus Intersect(LineSegment l1, LineSegment l2)
This method takes two line segments and computes the locus of points that is their intersection -- either empty, a single point, or a line segment.

Note that you can then generalize this method. Computing the intersection of a line segment with a line segment is tricky, but computing the intersection of a line segment with a point, or a point with a point, or anything with the empty locus is easy. And it's not hard to extend intersection to arbitrary unions of loci. Therefore, you could actually write:

static Locus Intersect(Locus l1, Locus l2)
And hey, now it becomes clear that Intersect could be an extension method on locus:

static Locus Intersect(this Locus l1, Locus l2)
Add an implicit conversion from PointF to PointLocus and LineSegment to LineSegmentLocus, and you can say things like

var point = new PointF(whatever);
var lineseg = new LineSegment(somepoint, someotherpoint);
var intersection = lineseg.Intersect(point);
if (intersection is EmptyLocus) ...
Using the type system well can massively improve the readability of a program.

shareimprove this answer
answered Feb 13 '10 at 16:08
community wiki
Eric Lippert
1	 	
Thanks for the recommendations and extensions. – Jared Updike Feb 13 '10 at 21:13
  	 	
This is a great method Eric, I was previously using enums combined with other objects to provide a result. This is elegant and far superior. Thank you. – Arcane Engineer Jul 25 '10 at 20:55
add a comment
up vote
11
down vote
    // port of this JavaScript code with some changes:
    //   http://www.kevlindev.com/gui/math/intersection/Intersection.js
    // found here:
    //   http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect/563240#563240

public class Intersector
{
    static double MyEpsilon = 0.00001;

    private static float[] OverlapIntervals(float ub1, float ub2)
    {
        float l = Math.Min(ub1, ub2);
        float r = Math.Max(ub1, ub2);
        float A = Math.Max(0, l);
        float B = Math.Min(1, r);
        if (A > B) // no intersection
            return new float[] { };
        else if (A == B)
            return new float[] { A };
        else // if (A < B)
            return new float[] { A, B };
    }

    // IMPORTANT: a1 and a2 cannot be the same, e.g. a1--a2 is a true segment, not a point
    // b1/b2 may be the same (b1--b2 is a point)
    private static PointF[] OneD_Intersection(PointF a1, PointF a2, PointF b1, PointF b2)
    {
        //float ua1 = 0.0f; // by definition
        //float ua2 = 1.0f; // by definition
        float ub1, ub2;

        float denomx = a2.X - a1.X;
        float denomy = a2.Y - a1.Y;

        if (Math.Abs(denomx) > Math.Abs(denomy))
        {
            ub1 = (b1.X - a1.X) / denomx;
            ub2 = (b2.X - a1.X) / denomx;
        }
        else
        {
            ub1 = (b1.Y - a1.Y) / denomy;
            ub2 = (b2.Y - a1.Y) / denomy;
        }

        List<PointF> ret = new List<PointF>();
        float[] interval = OverlapIntervals(ub1, ub2);
        foreach (float f in interval)
        {
            float x = a2.X * f + a1.X * (1.0f - f);
            float y = a2.Y * f + a1.Y * (1.0f - f);
            PointF p = new PointF(x, y);
            ret.Add(p);
        }
        return ret.ToArray();
    }

    private static bool PointOnLine(PointF p, PointF a1, PointF a2)
    {
        float dummyU = 0.0f;
        double d = DistFromSeg(p, a1, a2, MyEpsilon, ref dummyU);
        return d < MyEpsilon;
    }

    private static double DistFromSeg(PointF p, PointF q0, PointF q1, double radius, ref float u)
    {
        // formula here:
        //http://mathworld.wolfram.com/Point-LineDistance2-Dimensional.html
        // where x0,y0 = p
        //       x1,y1 = q0
        //       x2,y2 = q1
        double dx21 = q1.X - q0.X;
        double dy21 = q1.Y - q0.Y;
        double dx10 = q0.X - p.X;
        double dy10 = q0.Y - p.Y;
        double segLength = Math.Sqrt(dx21 * dx21 + dy21 * dy21);
        if (segLength < MyEpsilon)
            throw new Exception("Expected line segment, not point.");
        double num = Math.Abs(dx21 * dy10 - dx10 * dy21);
        double d = num / segLength;
        return d;
    }

    // this is the general case. Really really general
    public static PointF[] Intersection(PointF a1, PointF a2, PointF b1, PointF b2)
    {
        if (a1.Equals(a2) && b1.Equals(b2))
        {
            // both "segments" are points, return either point
            if (a1.Equals(b1))
                return new PointF[] { a1 };
            else // both "segments" are different points, return empty set
                return new PointF[] { };
        }
        else if (b1.Equals(b2)) // b is a point, a is a segment
        {
            if (PointOnLine(b1, a1, a2))
                return new PointF[] { b1 };
            else
                return new PointF[] { };
        }
        else if (a1.Equals(a2)) // a is a point, b is a segment
        {
            if (PointOnLine(a1, b1, b2))
                return new PointF[] { a1 };
            else
                return new PointF[] { };
        }

        // at this point we know both a and b are actual segments

        float ua_t = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
        float ub_t = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
        float u_b = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);

        // Infinite lines intersect somewhere
        if (!(-MyEpsilon < u_b && u_b < MyEpsilon))   // e.g. u_b != 0.0
        {
            float ua = ua_t / u_b;
            float ub = ub_t / u_b;
            if (0.0f <= ua && ua <= 1.0f && 0.0f <= ub && ub <= 1.0f)
            {
                // Intersection
                return new PointF[] {
                    new PointF
					(a1.X + ua * (a2.X - a1.X),
                        a1.Y + ua * (a2.Y - a1.Y)) };
            }
            else
            {
                // No Intersection
                return new PointF[] { };
            }
        }
        else // lines (not just segments) are parallel or the same line
        {
            // Coincident
            // find the common overlapping section of the lines
            // first find the distance (squared) from one point (a1) to each point
            if ((-MyEpsilon < ua_t && ua_t < MyEpsilon)
               || (-MyEpsilon < ub_t && ub_t < MyEpsilon))
            {
			if (a1.Equals(a2)) // danger!
                    return OneD_Intersection(b1, b2, a1, a2);
                else // safe
                    return OneD_Intersection(a1, a2, b1, b2);
            }
            else
            {
                // Parallel
                return new PointF[] { };
            }
        }
    }

}