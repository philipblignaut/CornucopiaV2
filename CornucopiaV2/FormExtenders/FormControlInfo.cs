using System.Collections.Generic;
using System.Windows.Forms;

namespace CornucopiaV2
{
	public class FormControlInfo
   {
      public Control Control { get; private set; }
      public object Value { get; private set; }
      public List<FormControlInfo> ControlList { get; private set; }
      public FormControlInfo
         (Control control
         , object value
         , List<FormControlInfo> controlList
         )
      {
         Control = control;
         Value = value;
         ControlList = controlList;
      }
      public bool CompareTypeName
         (FormControlInfo that
         )
      {
         return
            Control.GetType().Name == that.Control.GetType().Name
            && Control.Name == that.Control.Name
            ;
      }
      public bool CompareValue
         (FormControlInfo that
         )
      {
         bool retVal = false;
         if (Value == null && that.Value == null)
         {
            // both null, so they are equal
            retVal = true;
         }
         else
         {
            if (Value == null || that.Value == null)
            {
               // one is null, so not equal
               retVal = false;
            }
            else
            {
               // return the .Equals() method value, == does not work
               // the .Equals methost MUST be overridden in
               // custom classes (list items)
               retVal = Value.Equals(that.Value);
            }
         }
         return retVal;
      }
      public override string ToString()
      {
         return
            Control.GetType().Name
            + " "
            + Control.Name
            + " "
            + (Value == null ? "null" : Value.ToString())
            ;
      }
   }
}