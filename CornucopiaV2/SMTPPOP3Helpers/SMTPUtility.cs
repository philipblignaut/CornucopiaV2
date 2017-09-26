using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Configuration;

namespace CornucopiaV2
{
    public static partial class SMTPUtility
    {
        private static readonly string
          defaultSmtpServerNameKey = "smtpservername"
          ;
        private static CachedDictionary<int, string>
          cachedSmtpServerName =
          new CachedDictionary<int, string>
          (new IntEqualityComparer()
          , key =>
          {
              string smtpServerName = string.Empty;
              ConfigurationManager
                .AppSettings
               .AllKeys
               .Each
               (appSettingKey =>
               {
                   if
                   (appSettingKey
                     .Equals
                       (defaultSmtpServerNameKey
                       , StringComparison.CurrentCultureIgnoreCase
                       )
                   )
                   {
                       smtpServerName =
                       ConfigurationManager
                       .AppSettings[appSettingKey];
                   }
               }
               )
               ;
              smtpServerName = smtpServerName.Trim();
              if (smtpServerName.Length > 0)
              {
                  return smtpServerName;
              }
              throw
              new SmtpException
                ("No <add key=\""
                + defaultSmtpServerNameKey
                + "\" value=\"...\" />"
                + " entry was found in the app.config or web.config file"
                )
                ;
          }
          , key =>
          {
              return key;
          }
          , smtpServerName =>
          {
              return smtpServerName;
          }
          , 60 * 60 * 2 // 2 hours
          , 0
          )
          ;
        public static void SendMail
          (string smtpServer
          , MailAddress from
          , MailAddress[] to
          , MailAddress[] cc
          , MailAddress[] bcc
          , string subject
          , string body
          , bool isBodyHtml
          , Attachment[] attachments
          )
        {
            if (from == null)
            {
                throw new SMTPException("From must be specified");
            }
            if
              ((to == null || to.Length == 0)
              && (cc == null || cc.Length == 0)
              && (bcc == null || bcc.Length == 0)
              )
            {
                throw new SMTPException("No To, Cc or Bcc specified");
            }
            if (subject == null)
            {
                throw new SMTPException("Subject is null");
            }
            if (body == null)
            {
                throw new SMTPException("Body is null");
            }
            MailMessage message = new MailMessage();
            message.From = from;
            if (to != null)
            {
                to
                  .Each
                  (address =>
                  {
                      message.To.Add(address);
                  }
                  )
                  ;
            }
            if (cc != null)
            {
                cc
                  .Each
                  (address =>
                  {
                      message.CC.Add(address);
                  }
                  )
                  ;
            }
            if (bcc != null)
            {
                bcc
                  .Each
                  (address =>
                  {
                      message.Bcc.Add(address);
                  }
                  )
                  ;
            }
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isBodyHtml;
            if (attachments != null)
            {
                attachments
                  .Each
                  (attachment =>
                  {
                      message.Attachments.Add(attachment);
                  }
                  )
                  ;
            }
            SmtpClient smtpClient = new SmtpClient(smtpServer);
            smtpClient.Send(message);
        }
        public static void SendMail
          (string smtpServer
          , MailAddress from
          , IEnumerable<MailAddress> to
          , IEnumerable<MailAddress> cc
          , IEnumerable<MailAddress> bcc
          , string subject
          , string body
          , bool isBodyHtml
          , IEnumerable<Attachment> attachments
          )
        {
            SendMail
              (smtpServer
              , from
              , to == null ? new MailAddress[0] : to.ToArray()
              , cc == null ? new MailAddress[0] : cc.ToArray()
              , bcc == null ? new MailAddress[0] : bcc.ToArray()
              , subject
              , body
              , isBodyHtml
              , attachments == null ? new Attachment[0] : attachments.ToArray()
              )
              ;
        }
        public static void SendMail
          (MailAddress from
          , IEnumerable<MailAddress> to
          , IEnumerable<MailAddress> cc
          , IEnumerable<MailAddress> bcc
          , string subject
          , string body
          , bool isBodyHtml
          , IEnumerable<Attachment> attachments
          )
        {
            string smtpServer = cachedSmtpServerName[0];
            SendMail
              (smtpServer
              , from
              , to
              , cc
              , bcc
              , subject
              , body
              , isBodyHtml
              , attachments
              )
              ;
        }
        /// <summary>
        /// Converts a string into IEnumerable&lt;MailAddress&gt;
        /// </summary>
        /// <param name="addressPartList">String to be parse.  String must be in the
        /// format AddressFormat[[|AddressFormat]].  See SMTPUtility.ConvertToMailAddress
        /// for the format of AddressFormat.
        /// Example:
        /// mgr@abc.com:The manager|sec@abc.com:The Secretary|dir@abc.com:The Director</param>
        /// <returns>IEnumerable&lt;MailAddress&gt;</returns>
        public static IEnumerable<MailAddress> ConvertToMailAddresses
          (this string addressPartList
          )
        {
            foreach
              (
                string addressPart
                in
                  addressPartList
                    .Split
                      (new string[] { "|" }
                      , StringSplitOptions.RemoveEmptyEntries
                      )
              )
            {
                yield return ConvertToMailAddress(addressPart);
            }
        }
        public static void Dispose()
        {
            cachedSmtpServerName.Clear();
        }
        /// <summary>
        /// Parses a string into a MailAddress
        /// </summary>
        /// <param name="adressPart">String must be in the format
        /// EmailAddress[:DisplayName]. The : and Displayname are optional</param>
        /// <returns>MailAddress</returns>
        public static MailAddress ConvertToMailAddress
          (this string adressPart
          )
        {
            string addressPart = string.Empty;
            string displayNamePart = string.Empty;
            string[] parts =
              adressPart
                .Split
                (new string[] { ":" }
                , StringSplitOptions.None
                )
                ;
            if (parts.Length >= 1)
            {
                addressPart = parts[0].Trim();
                if (addressPart.Length == 0)
                {
                    throw
                      new SmtpException
                        ("E-mail address must be spcified"
                        )
                        ;
                }
                if (parts.Length >= 2)
                {
                    displayNamePart = parts[1].Trim();
                }
                return
                  new MailAddress
                    (addressPart
                    , displayNamePart
                    )
                    ;
            }
            else
            {
                throw
                  new SmtpException
                    ("No e-mail address found in " + addressPart
                    )
                    ;
            }
        }
        public static Attachment CreateAttachmentFromString
           (string attachmentData
           , string attachmentFileName
           )
        {
            byte[] attachmentBytes = Encoding.UTF8.GetBytes(attachmentData);
            MemoryStream memStream = new MemoryStream();
            memStream.Write(attachmentBytes, 0, attachmentBytes.Length);
            memStream.Flush();
            memStream.Seek(0, 0);
            return
               new Attachment
               (memStream
               , attachmentFileName
               )
               ;

        }
        public static void ClearCache()
        {
            cachedSmtpServerName.Clear();
        }
    }
}