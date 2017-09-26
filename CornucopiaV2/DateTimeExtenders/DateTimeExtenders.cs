using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CornucopiaV2
{
   public static class DateTimeExtenders
   {
      public static DateTime FirstDayInMonth
         (this DateTime dateTime
         )
      {
         //Debug.Print("FirstDayInMonth " + dateTime.ToCCYYMMDDHHMMSSString());
         while (dateTime.Day != 1)
         {
            dateTime = dateTime.AddDays(-1);
         }
         return dateTime;
      }
      public static DateTime LastDayInMonth
         (this DateTime dateTime
         )
      {
         //Debug.Print("LastDayInMonth " + dateTime.ToCCYYMMDDHHMMSSString());
         return dateTime.FirstDayInNextMonth().AddDays(-1);
      }
      public static DateTime FirstDayInPreviousMonth
         (this DateTime dateTime
         )
      {
         //Debug.Print("FirstDayInPreviousMonth " + dateTime.ToCCYYMMDDHHMMSSString());
         return dateTime.LastDayInPreviousMonth().FirstDayInMonth();
      }
      public static DateTime LastDayInPreviousMonth
         (this DateTime dateTime
         )
      {
         //Debug.Print("LastDayInPreviousMonth " + dateTime.ToCCYYMMDDHHMMSSString());
         int month = dateTime.Month;
         while (dateTime.Month == month)
         {
            dateTime = dateTime.AddDays(-1);
         }
         return dateTime;
      }
      public static DateTime FirstDayInNextMonth
         (this DateTime dateTime
         )
      {
         //Debug.Print("FirstDayInNextMonth " + dateTime.ToCCYYMMDDHHMMSSString());
         int month = dateTime.Month;
         while (dateTime.Month == month)
         {
            dateTime = dateTime.AddDays(1);
         }
         return dateTime;
      }
      public static DateTime LastDayInNextMonth
         (this DateTime dateTime
         )
      {
         //Debug.Print("LastDayInNextMonth " + dateTime.ToCCYYMMDDHHMMSSString());
         return dateTime.FirstDayInNextMonth().LastDayInMonth();
      }
      public static List<DateTime> DateListFromToIncluding
         (this DateTime startDate
         , DateTime endDate
         )
      {
         List<DateTime> dateList = new List<DateTime>();
         startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day);
         endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);
         if (endDate.ToCCYYMMDDValue() >= startDate.ToCCYYMMDDValue())
         {
            while (startDate.ToCCYYMMDDValue() <= endDate.ToCCYYMMDDValue())
            {
               dateList.Add(startDate);
               startDate = startDate.AddDays(1);
            }
         }
         return dateList;
      }
      public static DateTime ToCCYYMMDDOnly
         (this DateTime date
         )
      {
         return new DateTime(date.Year, date.Month, date.Day);
      }
      public static string ToCCYYMMDDString
         (this DateTime dateTime
         )
      {
         return dateTime.ToString("yyyy/MM/dd");
      }
        public static string ToCCYYMMDDHHMMSSString
           (this DateTime dateTime
           )
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }
        public static string ToCCYYMMDDHHMMSHyphenSString
           (this DateTime dateTime
           )
        {
            return dateTime.ToString("yyyy-MM-dd HH-mm-ss");
        }
        public static int ToCCYYMMDDValue
         (this DateTime dateTime
         )
      {
         return
            dateTime.Year * 10000
            + dateTime.Month * 100
            + dateTime.Day
            ;

      }
      public static string MonthNameEnglish
         (this DateTime dateTime
         )
      {
         switch (dateTime.Month)
         {
            case 1: return "January";
            case 2: return "February";
            case 3: return "March";
            case 4: return "April";
            case 5: return "May";
            case 6: return "June";
            case 7: return "July";
            case 8: return "August";
            case 9: return "September";
            case 10: return "October";
            case 11: return "November";
            default: return "December";
         }
      }
      public static string MonthShortNameEnglish
         (this DateTime dateTime
         )
      {
         switch (dateTime.Month)
         {
            case 1: return "Jan";
            case 2: return "Feb";
            case 3: return "Mar";
            case 4: return "Apr";
            case 5: return "May";
            case 6: return "Jun";
            case 7: return "Jul";
            case 8: return "Aug";
            case 9: return "Sep";
            case 10: return "Oct";
            case 11: return "Nov";
            default: return "Dec";
         }
      }
   }
}
