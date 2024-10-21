using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels.Report
{
    public class ProductReportViewModel
    {
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string LowStockStatus { get; set; }

    }
}
