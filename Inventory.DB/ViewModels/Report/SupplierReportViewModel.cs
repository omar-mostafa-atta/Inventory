using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels.Report
{
    public class SupplierReportViewModel
    {
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int ProductCount { get; set; }
        public List<string> ProductNames { get; set; }
    }
}
