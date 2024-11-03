
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidTA.XmlUtils.XmlModels;
using SolidTA.DAL.Entities;
using SolidTA.Converters;
using System.Security.Policy;

namespace SolidTA.DAL.MapProfiles
{
    public class RateMapper
    {
        private RateMapper() { }
        public static bool Map(ValCurs valCurs, out List<Rate> rateList)
        {
            rateList = new List<Rate>();
            if (valCurs == null 
                || valCurs.Valute == null 
                || !valCurs.Valute.Any())
                return false;
            DateTime dateTime;
            var dateParseCheck = DateTime.TryParse(valCurs.Date, out dateTime);
            if (!dateParseCheck)
                return false;

            short nominal;
            decimal value;

            foreach (var valute in valCurs.Valute)
            {
                if (!ToShortConverter.Convert(valute.Nominal, out nominal)
                    || !decimal.TryParse(valute.Value, out value) 
                    || string.IsNullOrWhiteSpace(valute.CharCode))
                    return false;
                Currency currency = new Currency()
                {
                    ID = valute.ID,
                    CharCode = valute.CharCode,
                    NumCode = valute.NumCode.ToString(),
                };
                Rate rate = new Rate()
                {
                    Date = dateTime,
                    Nominal = nominal,
                    Currency = currency,
                    Value=value

                };
                rateList.Add(rate);
            }
            return true;
        }
    }
}
