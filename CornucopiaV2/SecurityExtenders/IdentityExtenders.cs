using System.Security.Principal;

namespace CornucopiaV2
{
	public static class SecurityExtenders
   {
      /// <summary>
      /// Casts an IIdentity object, like
      /// HttpContext.Current.User.Identity
      /// to a WindowsIdentity object
      /// </summary>
      /// <param name="identity">IIdentity object</param>
      /// <returns>WindowsIdentity object</returns>
      public static WindowsIdentity WindowsIdentity
         (this IIdentity identity
         )
      {
         return (WindowsIdentity)identity;
      }
      /// <summary>
      /// Translates an IdentityReference object, like
      /// a group reference in a &lt;WindowsIdentity&gt;.Groups
      /// collection to an NTAccount object in order to get the
      /// NTAccount name and not just the reference
      /// </summary>
      /// <param name="identityReference">IdentityReference object</param>
      /// <returns>NTAccount object</returns>
      /// <exception cref="IdentityNotMappedException">Thrown when the reference
      /// could not be mapped
      /// </exception>
      public static NTAccount NTAccount
         (this IdentityReference identityReference
         )
      {
         return
            (NTAccount)
            identityReference
            .Translate
            (typeof(NTAccount)
            )
            ;
      }
      public static WindowsIdentityGroups WindowsIdentityGroups
         (this WindowsIdentity windowsIdentity
         )
      {
         return new WindowsIdentityGroups(windowsIdentity);
      }
   }
}
