using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidTA.Interfaces
{
    public interface IExcelExporter: IExport
    {
        void SetExportData(IEnumerable<IExcelModel> exportData);
    }
}
