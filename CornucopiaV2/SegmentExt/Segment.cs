using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
	public struct Segment
	{
		public int Index { get; internal set; }
		public double Value { get; private set; }
		/// <summary>Record Constructor</summary>
		/// <param name="index"><see cref="Index"/></param>
		/// <param name="value"><see cref="Value"/></param>
		public Segment(int index, double value)
		{
			Index = index;
			Value = value;
		}

		public override string ToString()
		{
			return
				"i " + Index.ToString()
				+ " v " + Value.ToString()
				;
		}

		public override bool Equals(object obj)
		{
			Segment other = new Segment();
			if (obj is Segment)
			{
				other = (Segment)obj;
				return
					Index == other.Index
					&& Value == other.Value
					;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return
				Index.GetHashCode()
				| Value.GetHashCode()
				;
		}

		public static bool operator ==(Segment left, Segment right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Segment left, Segment right)
		{
			return !(left == right);
		}
	}
}