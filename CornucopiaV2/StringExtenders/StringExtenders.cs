using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CornucopiaV2;
using System.Web;

namespace CornucopiaV2
{
    public static class StringExtenders
    {
        public static readonly string es = string.Empty;
        public static string Left
            (this string data
            , int charcount
            )
        {
            string result = es;
            char[] chars = es.ToCharArray();
            for (int index = 0; index < Math.Min(charcount, data.Length); index++)
            {
                result += chars[index].ToString();
            }
            return result;
        }
        public static string Mid
            (this string data
            , int start
            , int charcount
            )
        {
            string result = es;
            char[] chars = es.ToCharArray();
            for (int index = start;index < Math.Min(charcount, data.Length); index++)
            {
                result += chars[index].ToString();
            }
            return result;
        }
        public static string HtmlEncode
           (this string textString
           )
        {
            return HttpUtility.HtmlEncode(textString);
        }
        public static string HtmlDecode
           (this string htmlString
           )
        {
            return HttpUtility.HtmlDecode(htmlString);
        }
        public static string NewLineToHtmlBreak
           (this string textString
           )
        {
            return
               textString
               .Replace
                  (Environment.NewLine
                  , "<br />"
                  )
               .Replace
                  ("\r"
                  , "<br />"
                  )
               .Replace
                  ("\n"
                  , "<br />"
                  )
                  ;
        }
        public static string HtmlBreakToNewLine
           (this string htmlString
           )
        {
            return
               htmlString
               .Replace
                  ("<br>"
                  , Environment.NewLine
                  )
               .Replace
                  ("<br />"
                  , Environment.NewLine
                  )
               .Replace
                  ("<br/>"
                  , Environment.NewLine
                  )
               .Replace
                  ("<BR>"
                  , Environment.NewLine
                  )
               .Replace
                  ("<BR />"
                  , Environment.NewLine
                  )
               .Replace
                  ("<BR/>"
                  , Environment.NewLine
                  )
               .Replace
                  ("<Br>"
                  , Environment.NewLine
                  )
               .Replace
                  ("<Br />"
                  , Environment.NewLine
                  )
               .Replace
                  ("<Br/>"
                  , Environment.NewLine
                  )
                 ;
        }
        public static string TextBetween
           (this string target
           , string startDelimiter
           , string endDelimiter
           )
        {
            string between = string.Empty;
            int pos = target.IndexOf(startDelimiter);
            if (pos >= 0)
            {
                target = target.Substring(pos + startDelimiter.Length);
                pos = target.IndexOf(endDelimiter);
                if (pos >= 0)
                {
                    between = target.Substring(0, pos);
                }
                else
                {
                    throw
                       new Exception
                          ("Cannot find end delimiter '" + endDelimiter + "' "
                          + "in '"
                          + (target.Length > 40 ? target.Substring(0, 40) : target)
                          + "...'"
                          )
                          ;
                }
            }
            return between;
        }
        public static List<string> MultipleTextBetween
           (this string target
           , string startDelimiter
           , string endDelimiter
           )
        {
            List<string> list = new List<string>();
            int pos = 0;
            while (pos >= 0)
            {
                pos = target.IndexOf(startDelimiter);
                if (pos >= 0)
                {
                    target = target.Substring(pos + startDelimiter.Length);
                    pos = target.IndexOf(endDelimiter);
                    if (pos >= 0)
                    {
                        list.Add(target.Substring(0, pos));
                        target = target.Substring(pos + endDelimiter.Length);
                    }
                    else
                    {
                        throw
                           new Exception
                              ("Cannot find end delimiter '" + endDelimiter + "' "
                              + "in '"
                              + (target.Length > 40 ? target.Substring(0, 40) : target)
                              + "...'"
                              )
                              ;
                    }
                    pos = 0;
                }
            }
            return list;
        }
        public static List<string> MultipleTextStartsWith
           (this string target
           , string delimiter
           , int length
           )
        {
            List<string> list = new List<string>();
            int pos = 0;
            while (pos >= 0)
            {
                pos = target.IndexOf(delimiter);
                if (pos >= 0)
                {
                    target = target.Substring(pos + delimiter.Length);
                    if (target.Length >= length)
                    {
                        list.Add(target.Substring(0, length));
                        target = target.Substring(length);
                    }
                    else
                    {
                        throw
                           new Exception
                              ("Cannot find enough characters ("
                              + length.ToString()
                              + ") after '" + delimiter + "' "
                              + "in '"
                              + (target.Length > 40 ? target.Substring(0, 40) : target)
                              + "...'"
                              )
                              ;
                    }
                    pos = 0;
                }
            }
            return list;
        }
        public static string RemoveTextBetweenIncluding
           (this string target
           , string startDelimiter
           , string endDelimiter
           )
        {
            int posS = target.IndexOf(startDelimiter);
            int posE = target.IndexOf(endDelimiter);
            if (posS > 0)
            {
                if (posE > posS)
                {
                    target =
                       target.Substring(0, posS)
                       + target.Substring(posE + endDelimiter.Length)
                       ;
                }
                else
                {
                    throw
                       new Exception
                          (endDelimiter + " not found after " + startDelimiter + " in RemoveTextBetweenIncluding"
                          )
                          ;
                }
            }
            else
            {
                throw
                   new Exception
                      (startDelimiter + " not found in RemoveTextBetweenIncluding"
                      )
                      ;
            }
            return target;
        }
        public static string ReplaceRepeat
           (this string target
           , string find
           , string replace
           )
        {
            return
               target.Contains(find)
               ? target.Replace(find, replace).ReplaceRepeat(find, replace)
               : target
               ;
        }
        public static string[] Split
           (this string target
           , string separator
           , StringSplitOptions options
           )
        {
            return
               target
               .Split
               (new string[] { separator }
               , options
               )
               ;
        }
        public static string[] SplitRemoveEmptyEntries
           (this string target
           , string separator
           )
        {
            return
               target
               .Split
               (separator
               , StringSplitOptions.RemoveEmptyEntries
               )
               ;
        }
        public static string[] SplitKeepEmptyEntries
           (this string target
           , string separator
           )
        {
            return
               target
               .Split
               (separator
               , StringSplitOptions.None
               )
               ;
        }
        /// <summary>
        /// A Split method with two seperators.
        /// </summary>
        /// <author> GS WILLIAMS </author>
        /// <returns></returns>
        /// <remarks>What exactly is this extender supposed to do?</remarks>
        public static string[] Split
          (this string target
          , string separatorOne
          , string separatorTwo
          , StringSplitOptions option
          )
        {
            return
              target
              .Split
               (separatorOne
               , separatorTwo
               , option
               )
            ;
        }
        public static string FormatWith
           (this string format
           , params object[] args
           )
        {
            return string.Format(format, args);
        }
        public static IEnumerable<string> SplitToCharacters
           (this string data
           )
        {
            for (int chr = 0; chr < data.Length; chr++)
            {
                yield return data.Substring(chr, 1);
            }
        }
        public static void EachCharacter
           (this string data
           , Action<string> characterPredicate
           )
        {
            for (int chr = 0; chr < data.Length; chr++)
            {
                characterPredicate.Invoke(data.Substring(chr, 1));
            }
        }
        public static string ToCSVFormat
           (this string data
           )
        {
            if (data == null)
            {
                return "";
            }
            if (data.IndexOf("\"") >= 0 || data.IndexOf(",") >= 0)
            {
                return "\"" + data.Replace("\"", "\\\"") + "\"";
            }
            return data;
        }
        public static string ToBase64
           (this string data
           )
        {
            return System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(data));
        }
        public static string FromBase64
           (this string base64data
           )
        {
            return ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(base64data));
        }
        public static string Repeat
           (this string data
           , int numberOfTimes
           )
        {
            string returnString = string.Empty;
            for (int rep = 0; rep < numberOfTimes; rep++)
            {
                returnString += data;
            }
            return returnString;
        }
    }
}
