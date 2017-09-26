using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
