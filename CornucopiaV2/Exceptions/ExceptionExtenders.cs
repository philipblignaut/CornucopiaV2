using System;

namespace CornucopiaV2
{
	public static class ExceptionExtenders
   {
      /// <summary>
      /// Returns a verbose description of the exception, including
      /// the Message, Source and StackTrace properties of the exception
      /// as well as that of any embedded InnerExceptions 
      /// </summary>
      /// <param name="ex">Any Exception</param>
      /// <returns>Verbose description of the error</returns>
      public static string VerboseMessage
        (this Exception ex
        )
      {
         string message = string.Empty;
         while (ex != null)
         {
            message +=
              (message.Length > 0
              ? (Environment.NewLine
                + "InnerException:"
                + Environment.NewLine
                )
              : string.Empty
              )
              + "Message:"
              + Environment.NewLine
              + ex.Message
              + Environment.NewLine
              + "Source:"
              + Environment.NewLine
              + ex.Source
              + Environment.NewLine
              + "StackTrace:"
              + Environment.NewLine
              + ex.StackTrace
              ;
            ex = ex.InnerException;
         }
         return message;
      }
   }
}
