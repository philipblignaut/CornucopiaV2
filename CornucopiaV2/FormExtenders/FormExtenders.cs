using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CornucopiaV2
{
   public static class FormExtenders
   {
      public static List<string> ControlTypeList { get; private set; }
      static FormExtenders()
      {
         ControlTypeList = new List<string>();
         ControlTypeList.Add(typeof(TextBox).Name);
         ControlTypeList.Add(typeof(ComboBox).Name);
         ControlTypeList.Add(typeof(ListBox).Name);
         ControlTypeList.Add(typeof(RadioButton).Name);
         ControlTypeList.Add(typeof(CheckBox).Name);
         ControlTypeList.Add(typeof(CheckedListBox).Name);
         // add more types here if required
      }
      public static void SetDefaultTypeList
         (List<Type> typeList
         )
      {
         ControlTypeList.Clear();
         typeList.Each(type => ControlTypeList.Add(type.Name));
      }
      public static List<FormControlInfo> CurrentControlCollectionValues
         (this Control control
         )
      {
         return
            control
               .Controls
               .Cast<Control>()
               .Convert
               (childCcontrol =>
                  new FormControlInfo
                     (childCcontrol
                     , childCcontrol.ControlValue()
                     , childCcontrol.CurrentControlCollectionValues()
                     )
               )
               .ToList()
               ;
      }
      public static List<FormControlInfo> CurrentControlCollectionValues
         (this Form form
         )
      {
         return ((Control)form).CurrentControlCollectionValues();
      }
      public static List<Control> Delta
         (this List<FormControlInfo> initialList
         , List<FormControlInfo> currentList
         )
      {
         List<Control> delta = new List<Control>();
         (
            from initItem in initialList
            from currItem in currentList
            where initItem.CompareTypeName(currItem)
            select new { initItem = initItem, currItem = currItem }
         )
         .Each
         (pair =>
            {
               if (ControlTypeList.Contains(pair.initItem.Control.GetType().Name))
               {
                  if (!pair.initItem.CompareValue(pair.currItem))
                  {
                     delta.Add(pair.initItem.Control);
                  }
               }
               delta.AddRange(pair.initItem.ControlList.Delta(pair.currItem.ControlList));
            }
         )
         ;
         return delta;
      }
      public static object ControlValue
         (this Control control
         )
      {
         object value = null;
         Func<Control, Type, bool> mustGetValue =
            ((ctrl, type) =>
               ctrl.GetType().FullName == type.FullName
               && ControlTypeList.Contains(type.Name)
            )
            ;
         if (mustGetValue(control, typeof(TextBox)))
         {
            value = ((TextBox)control).Text;
         }
         if (mustGetValue(control, typeof(ComboBox)))
         {
            value = ((ComboBox)control).SelectedItem;
         }
         if (mustGetValue(control, typeof(ListBox)))
         {
            value = ((ListBox)control).SelectedItem;
         }
         if (mustGetValue(control, typeof(RadioButton)))
         {
            value = ((RadioButton)control).Checked;
         }
         if (mustGetValue(control, typeof(CheckBox)))
         {
            value = ((CheckBox)control).Checked;
         }
         if (mustGetValue(control, typeof(CheckedListBox)))
         {
            value = ((CheckedListBox)control).CheckedItems;
         }
         // add more value extractions here if required
         return value;
      }
   }
}