using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels.Report
{
    public class StockReportViewModel
    {
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
