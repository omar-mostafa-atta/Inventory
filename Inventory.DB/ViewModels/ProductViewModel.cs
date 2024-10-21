using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Inventory.DB.ViewModels
{
    [Keyless]
    public class ProductViewModel
    {

        [Required(ErrorMessage = "Product name is required.")]
        [MinLength(3, ErrorMessage = "Product name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Product name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long.")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative integer.")]
        public int StockQuantity { get; set; }

        [RegularExpression(@"(.*\.(jpg|jpeg|png|gif|bmp|webp))$", ErrorMessage = "Only image files are allowed (.jpg, .jpeg, .png, .gif, .bmp, .webp)")]
        public string imageurl { get; set; }

		[Required(ErrorMessage = "LowStockThreshold  is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "LowStockThreshold must be a non-negative integer.")]
        public int LowStockThreshold { get; set; }
        [Display(Name = "Category")]

        public IEnumerable<SelectListItem> categories { get; set; }
        [Display(Name = "Supplier")]
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }


        public IEnumerable<SelectListItem> Suppliers { get; set; }
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

		 
	}


}
