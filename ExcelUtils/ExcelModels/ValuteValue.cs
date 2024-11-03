using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTA.ExcelUtils.ExcelModels
{
    public class ValuteValue
    {
        public string CharCode { get; set; }
        public decimal Value { get; set; }
        public short Nominal { get; set; }
        public ValuteValue(string charCode, decimal value, short nominal)
        {
            CharCode = charCode;
            Value = value;
            Nominal = nominal;
        }
    }
}
