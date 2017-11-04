using System;

namespace CornucopiaV2
{

	public abstract class OffsetAttribute : Attribute
	{
		public float Offset { get; private set; }
		public OffsetAttribute(float offset) { Offset = offset; }
	}

}