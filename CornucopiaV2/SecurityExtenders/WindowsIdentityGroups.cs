using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace CornucopiaV2
{
   public class WindowsIdentityGroups
   {
      public class IdentityReferenceName
      {
         public string Reference { get; private set; }
         public string Name { get; private set; }
         protected internal IdentityReferenceName
            (string reference
            , string name
            )
         {
            Reference = reference;
            Name = name;
         }
      }
      public string IdentityName { get; private set; }
      public List<IdentityReferenceName> Groups { get; private set; }
      protected internal WindowsIdentityGroups
         (WindowsIdentity windowsIdentity
         )
      {
         IdentityName = windowsIdentity.Name;
         Groups = new List<IdentityReferenceName>();
         windowsIdentity
            .Groups
            .Cast<IdentityReference>()
            .Each
            (idRef =>
            {
               if (idRef.IsValidTargetType(typeof(NTAccount)))
               {
                  try
                  {
                     NTAccount acc = idRef.NTAccount();
                     Groups.Add(new IdentityReferenceName(idRef.Value, acc.Value));
                  }
                  catch (Exception ex)
                  {
                     Groups.Add(new IdentityReferenceName(idRef.Value, ex.Message));
                  }
               }
               else
               {
                  Groups.Add(new IdentityReferenceName(idRef.Value, string.Empty));
               }
            }
            )
            ;
         Groups = Groups.OrderBy(group => group.Name).ToList();
      }
   }
}