using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SolidTA.Commons
{
    public class URL
    {
        private URL() { }
        private static string _url = "http://www.cbr.ru/scripts/XML_daily.asp?";
        public static string Url { get { return _url;} }
        public static string GetUriString(DateTime dateTime)
        {
            return _url + $"date_req={dateTime.Day.ToString("00")}/{dateTime.Month.ToString("00")}/{dateTime.Year}";
        }
    }
}
