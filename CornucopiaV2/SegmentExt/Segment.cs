using System;

namespace CornucopiaV2
{
	public struct Segment
		: IEquatable<Segment>
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

		public bool Equals(Segment other)
			=> Index == other.Index
			&& Value == other.Value
			;

		public override bool Equals(object obj)
			=> obj is Segment other
			&& Equals(other)
			;

		public static bool operator ==(Segment left, Segment right)
			=> left.Equals(right);

		public static bool operator !=(Segment left, Segment right)
			=> !left.Equals(right);

		public override int GetHashCode()
			=> Index.GetHashCode()
				& Value.GetHashCode()
				;

		public override string ToString()
			=> $"i {Index} v {Value}";

	}
}