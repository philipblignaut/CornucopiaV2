using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace CornucopiaV2
{
   public static class WebFormExtenders
   {
      /// <summary>
      /// Process Name Value Pairs
      /// Eg. Request.QueryString
      /// </summary>
      /// <param name="nameValueCollection"></param>
      /// <param name="kvpActionParams">List of KVPAction objects</param>
      public static void ParseNameValuePairs
         (this NameValueCollection nameValueCollection
         , params KVPAction[] kvpActionParams
         )
      {
         nameValueCollection
            .AllKeys
            .Each
            (key =>
               kvpActionParams
               .Where(kvpA => kvpA.Key == key)
               .Each
               (kvpA =>
               {
                  kvpA.Action.Invoke(nameValueCollection[key]);
               }
               )
            )
            ;
      }
      public static List<T> FindControlsIDStartsWith<T>
         (this Control control
         , string idStartsWith
         )
         where T : Control
      {
         return
            control
            .FindControls<T>()
            .Where(child => child.ID != null && child.ID.StartsWith(idStartsWith))
            .ToList()
            ;
      }
      public static List<T> FindControlsIDEquals<T>
         (this Control control
         , string idEquals
         )
         where T : Control
      {
         return
            control
            .FindControls<T>()
            .Where(child => child.ID != null && child.ID.Equals(idEquals))
            .ToList()
            ;
      }
      public static List<T> FindControls<T>
         (this Control control
         )
         where T : Control
      {
         List<T> list = new List<T>();
         FindControls(control, list);
         return list;
      }
      private static void FindControls<T>
         (this Control control
         , List<T> list
         )
         where T : Control
      {
         if (control is T)
         {
            list.Add((T)control);
         }
         if (control.HasControls())
         {
            control
               .Controls
               .Cast<Control>()
               .Each
               (child =>
                  FindControls
                  (child
                  , list
                  )
               )
               ;
         }
      }
      public static void Each
         (this ListItemCollection collection
         , Action<ListItem> action
         )
      {
         foreach (ListItem item in collection)
         {
            action.Invoke(item);
         }
      }
   }
}
