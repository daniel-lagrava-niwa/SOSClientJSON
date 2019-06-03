using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSClientJSON.Utils
{
    public class TimeFormat
    {
        public static string GetTimeFormatForQuery(int year, int month, int day, int hour=0, int minutes=0, int seconds=0)
        {
            DateTime date = new DateTime(year, month, day, hour, minutes, seconds, DateTimeKind.Local);
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
//          return string.Format("{0:o}", date);
        }
    }
}
