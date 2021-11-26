using System.Globalization;
using AUA.ProjectName.Common.Consts;

namespace AUA.ProjectName.Common.Extensions
{
    public static class DateTimeExtension
    {

        public static string ToPersianDate(this DateTime date)
        {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
                return "";
            var persianCalendar = new PersianCalendar();
            return ToPersianDate(persianCalendar.GetYear(date), persianCalendar.GetMonth(date), persianCalendar.GetDayOfMonth(date));
        }


        public static string ToPersianDateTime(this DateTime date, bool showSecond = true)
        {
            try
            {
                var persianCalendar = new PersianCalendar();
                return
                    $"{ToPersianDate(persianCalendar.GetYear(date), persianCalendar.GetMonth(date), persianCalendar.GetDayOfMonth(date))} ({date.ToShortTimeString(showSecond)})";
            }
            catch
            {
                return "";
            }
        }

        public static string ToShortTimeString(this DateTime date, bool showSecond = false)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string str = string.Format("{0:d2}:{1:d2}", persianCalendar.GetHour(date), persianCalendar.GetMinute(date));
            return showSecond ? string.Format("{0}:{1:d2}", str, persianCalendar.GetSecond(date)) : str;
        }


        public static DateTime ToDateTime(this string persianDate)
        {
            var persianCalendar = new PersianCalendar();
            var strArray = persianDate.Split(AppConsts.SplitDateTimeChar, StringSplitOptions.RemoveEmptyEntries);
            var year = int.Parse(strArray[0]);
            var month = int.Parse(strArray[1]);
            var day = int.Parse(strArray[2]);


            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public static string ToPersianDate(int persianYear, int persianMonth, int persianDay)
        {
            return string.Format("{0:d2}/{1:d2}/{2:d2}", persianYear, persianMonth, persianDay);
        }


    }
}
