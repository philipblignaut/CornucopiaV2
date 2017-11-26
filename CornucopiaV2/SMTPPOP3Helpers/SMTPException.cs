using System;

namespace CornucopiaV2
{
	public class SMTPException : Exception
   {
      public SMTPException
        (string message
        )
         : base(message)
      {
      }
   }
}
