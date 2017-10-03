using System;
using System.Runtime.Serialization;

namespace CornucopiaV2
{
	public abstract class ExceptionOfBase : Exception
	{

		protected ExceptionOfBase()
			: base() { }

		protected ExceptionOfBase(string message)
			: base(message) { }

		protected ExceptionOfBase
			(string message
			, Exception innerException
			)
			: base
			(message
			, innerException
			)

		{
		}

		protected ExceptionOfBase
			(SerializationInfo info
			, StreamingContext context
			)
			: base
			(info
			, context
			)
		{
		}

	}

}