using SolidTA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
namespace SolidTA.ExcelUtils.ExcelExporters
{
    public class CrosCursExporter:IExcelExporter
    {
        private List<IExcelModel> ExportData { get; set; } 
        public bool Export(string path)
        {
            if (ExportData == null || !ExportData.Any())
                return false;
            using (var workbook = new XLWorkbook())
            {
                foreach (var model in ExportData)
                {
                    var worksheet = workbook.AddWorksheet(model.Valute);
                    worksheet.Cell("A1").Value = "Валюта";
                    worksheet.Cell("B1").Value = model.Valute;
                    worksheet.Cell("A2").Value = "Дата";
                    worksheet.Cell("B2").Value = model.Date;
                    worksheet.Cell("A3").Value = "Букв. код";
                    worksheet.Cell("B3").Value = "Единиц";
                    worksheet.Cell("C3").Value = "Курс";
                    int i = 4;
                    foreach(var crocCurs in model.CrosCurs)
                    {
                        worksheet.Cell($"A{i}").Value = crocCurs.CharCode;
                        worksheet.Cell($"B{i}").Value = crocCurs.Nominal;
                        worksheet.Cell($"C{i}").Value = crocCurs.Value;
                        i++;
                    }
                }
                try
                {
                    workbook.SaveAs(path);
                }
                catch (Exception ex)
                {
                    return false;
                }
                
                return true;
            }

           
        }

        public void SetExportData(IEnumerable<IExcelModel> exportData)
        {
            ExportData = exportData.ToList();
        }
    }
}
