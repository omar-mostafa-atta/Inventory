using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.DB.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format. Please use the format: example@domain.com")]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(012|011|010|015)\d{8}$", ErrorMessage = "Phone number is Invalid, it must be 11 characters and starts with (012|011|010|015) ")]
        [StringLength(11, ErrorMessage = "it cannot be longer than 11 characters")]
        public string? Phone { get; set; }
        [RegularExpression(@"(.*\.(jpg|jpeg|png|gif|bmp|webp))$", ErrorMessage = "Only image files are allowed (.jpg, .jpeg, .png, .gif, .bmp, .webp)")]

        public string? imageurl { get; set; }
        public string role { get; set; }


        [Display(Name = "Role")]

        public IEnumerable<SelectListItem> Roles { get; set; }

    }
}
