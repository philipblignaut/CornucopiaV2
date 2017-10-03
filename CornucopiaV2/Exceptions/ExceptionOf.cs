using System;
using System.Runtime.Serialization;

namespace CornucopiaV2
{
	public class ExceptionOf<T> : ExceptionOfBase
	{
		public ExceptionOf()
				: base() { }
		public ExceptionOf(string message)
				: base(message) { }
		public ExceptionOf(string message, Exception innerException)
				: base(message, innerException) { }
		public ExceptionOf(SerializationInfo info, StreamingContext context)
				: base(info, context) { }
	}
}