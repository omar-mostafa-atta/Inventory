using Inventory.DB.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Service.Sevices.Reports
{
    public interface IReportService
    {
        public CombinedReportViewModel GenerateCombinedReport();
    }
}
