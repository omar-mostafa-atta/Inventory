using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels
{
    public class TransactionViewModel
    {
        public string TransactionType { get; set; }
        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative integer.")]
        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public int ProductID { get; set; }
        public Product Product { get; set; }
        [Display(Name = "Product")]

        public IEnumerable<SelectListItem> products { get; set; }
        public int? SelectedProductId { get; set; }
    }
}
