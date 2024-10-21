using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels
{
    public class CategoryViewModel
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name is required.")]
        [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string CategoryName { get; set; }

        [Display(Name = "Description")]
        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string Description { get; set; }

        [RegularExpression(@"(.*\.(jpg|jpeg|png|gif|bmp|webp))$", ErrorMessage = "Only image files are allowed (.jpg, .jpeg, .png, .gif, .bmp, .webp)")]
        public string imageurl { get; set; }
    }
}
