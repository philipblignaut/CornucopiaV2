using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace CornucopiaV2
{
   public static class FormControlExtenders
   {
      public static void InvokeOnUIThread
         (this Control control
         , Action action
         )
      {
         if (control.InvokeRequired)
         {
            control.Invoke(action);
         }
         else
         {
            action.Invoke();
         }
      }
   }
}