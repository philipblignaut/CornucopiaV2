using System;
using System.Security.Principal;

namespace CornucopiaV2
{
	public enum UserStaffMemberResultType
   {
      [EnumLongDescription("Is Staff Member")]
      IsStaffMember = 1,
      [EnumLongDescription("Identity not on Nedcor domain")]
      IdentityNotOnNedcorDomain = 32,
      [EnumLongDescription("User code does not start with NB, CC or ADM")]
      IdentityNotNBCCOrADM = 64,
      [EnumLongDescription("User code does not end in six digits")]
      IdentitySuffixNotSixDigits = 128,
      [EnumLongDescription("Unknown error while trying to check if Identity is Staff Member")]
      Unknown = 256,
   }
   public class UserStaffMemberResult
   {
      public string IdentityName { get; private set; }
      public string StaffNumber { get; private set; }
      public UserStaffMemberResultType Result { get; private set; }
      protected internal UserStaffMemberResult
         (string identityName
         , string staffNumber
         , UserStaffMemberResultType result
         )
      {
         IdentityName = identityName;
         StaffNumber = staffNumber;
         Result = result;
      }
   }
   public static class UserStaffNumberChecker
   {
      private static readonly string digits = "0123456789";
      public static UserStaffMemberResult IsStaffMember
         (this WindowsIdentity windowsIdentity
         )
      {
         return windowsIdentity.Name.IsStaffMember();
      }
      /// <summary>
      /// Returns an object indicating if the parameter passed
      /// is a Nedbank staff member
      /// <br />example:
      /// <br />
      /// HttpContext.Current.Request.LogonUserIdentity.Name
      /// .IsStaffMember()
      /// </summary>
      /// <param name="identityName"></param>
      /// <returns>object of type UserStaffMemberResult</returns>
      public static UserStaffMemberResult IsStaffMember
         (this string identityName
         )
      {
         string staffNumber = string.Empty;
         UserStaffMemberResultType result = UserStaffMemberResultType.Unknown;
         identityName = identityName.ToLower();
         if (identityName.StartsWith("nedcor\\"))
         {
            string[] identityParts =
               identityName.Split(new string[] { "\\" }, StringSplitOptions.None)
               ;
            string usercode =
               string.Join
               ("\\"
               , identityParts
               , 1
               , identityParts.Length - 1
               )
               ;
            if
               (
                  (usercode.StartsWith("nb")
                  || usercode.StartsWith("cc")
                  || usercode.StartsWith("adm")
                  )
               )
            {
               usercode = usercode.Substring(2);
               if (usercode.StartsWith("m"))
               {
                  usercode = usercode.Substring(1);
               }
               if
                  (usercode.Length == 6
                  && digits.IndexOf(usercode.Substring(0, 1)) >= 0
                  && digits.IndexOf(usercode.Substring(1, 1)) >= 0
                  && digits.IndexOf(usercode.Substring(2, 1)) >= 0
                  && digits.IndexOf(usercode.Substring(3, 1)) >= 0
                  && digits.IndexOf(usercode.Substring(4, 1)) >= 0
                  && digits.IndexOf(usercode.Substring(5, 1)) >= 0
                  )
               {
                  staffNumber = usercode;
                  result =
                     UserStaffMemberResultType
                     .IsStaffMember
                     ;
               }
               else
               {
                  result =
                     UserStaffMemberResultType
                     .IdentitySuffixNotSixDigits
                     ;
               }
            }
            else
            {
               result =
                  UserStaffMemberResultType
                  .IdentityNotNBCCOrADM
                  ;
            }
         }
         else
         {
            result =
              UserStaffMemberResultType
              .IdentityNotOnNedcorDomain
              ;
         }
         return new
            UserStaffMemberResult
            (identityName
            , staffNumber
            , result
            )
            ;
      }
   }
}