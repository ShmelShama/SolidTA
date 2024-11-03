using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidTA.DAL.Entities;
using SolidTA.Interfaces;

namespace SolidTA.ExcelUtils.ExcelModels
{
    public class ExcelCursModel: IExcelModel
    {
        public string Valute { get; set; }
        public string Date { get; set; }
        public short Nominal { get; set; } 
        public decimal CurrCurs { get; set; } 
        public List<ValuteValue> CrosCurs { get; }
        public ExcelCursModel(string valute, string date, short nominal, decimal currCurs)
        {
            Valute = valute;
            Date = date;
            CurrCurs = currCurs;
            Nominal = nominal;
            CrosCurs = new List<ValuteValue> { };
        }

  

        public void SetCrossCurs(List<Rate> rates)
        {
            CrosCurs.Clear();
            foreach (Rate rate in rates)
            {
                if (rate.Currency.CharCode == Valute)
                    continue;
                CrosCurs.Add(new ValuteValue(rate.Currency.CharCode, Math.Round(rate.Value * (Nominal / CurrCurs),4,MidpointRounding.AwayFromZero), rate.Nominal));
            }
        }

    }
}
