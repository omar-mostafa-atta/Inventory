using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DB.ViewModels
{
    public class AlertViewModel
    {
        public DateTime AlertDate { get; set; }
        [Required]
        [MinLength(20), MaxLength(200)]
        public string Description { get; set; }

        public bool IsResolved { get; set; }
        public IEnumerable<SelectListItem> products { get; set; }
    }
}
