using SolidTA.DAL.Entities;
using SolidTA.ExcelUtils.ExcelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTA.Interfaces
{
    public interface IExcelModel
    {
        string Valute { get; set; }
        string Date { get; set; }
        short Nominal { get; set; }
        decimal CurrCurs { get; set; }
        List<ValuteValue> CrosCurs { get;  }
        void SetCrossCurs(List<Rate> rates);
    }
}
