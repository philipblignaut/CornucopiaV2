using System;

namespace CornucopiaV2
{
	public abstract class AngleAttribute : Attribute
	{
		public float Angle { get; private set; }
		public AngleAttribute(float angle) { Angle = angle; }
	}

}