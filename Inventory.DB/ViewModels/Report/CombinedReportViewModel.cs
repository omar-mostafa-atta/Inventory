using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels.Report
{
    public class CombinedReportViewModel
    {
        public IEnumerable<StockReportViewModel> StockReports { get; set; } = new List<StockReportViewModel>();
        public IEnumerable<ProductReportViewModel> ProductReports { get; set; } = new List<ProductReportViewModel>();
        public IEnumerable<SupplierReportViewModel> SupplierReports { get; set; } = new List<SupplierReportViewModel>();
    }
}
