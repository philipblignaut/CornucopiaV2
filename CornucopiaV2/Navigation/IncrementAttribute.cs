using System;

namespace CornucopiaV2
{

	public abstract class IncrementAttribute : Attribute
	{
		public int Unit { get; private set; }

		protected IncrementAttribute(int unit) { Unit = unit; }
	}

}