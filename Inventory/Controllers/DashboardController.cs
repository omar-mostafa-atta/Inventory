using Inventory.Service.Sevices.CategoryService;
using Inventory.Service.Sevices.ProductService;
using Inventory.Service.Sevices.SupplierService;
using Inventory.Service.Sevices.TransactionService;
using Inventory.Service.Sevices.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
	[Authorize]
	[Authorize(Roles = "admin")]
	public class DashboardController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService; 
        private readonly ISupplierService _supplierService; 
        private readonly IUserService _userService; 

        public DashboardController(IProductService productService,
            ICategoryService categoryService,
            ISupplierService supplierService,
            IUserService userService,
            ITransactionService transactionService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _userService = userService;
        }

        public IActionResult DisplayDash()
        {
            var productCount = _productService.GetAll().Count();
            var categoryCount = _categoryService.GetAll().Count();
            var usersCount = _userService.GetAll().Count();
            var supplierCount = _supplierService.GetAll().Count();


            var ProductsWithMostStocked = _productService.GetAll()
                .OrderByDescending(p => p.StockQuantity) 
                .ToList();

            var categoriesWithMostProducts = _categoryService.GetAll()
                .Select(c => new
                {
                    Category = c,
                    ProductCount = _productService.GetAll().Count(p => p.CategoryID == c.ID)
                })
                .OrderByDescending(c => c.ProductCount)
                .ToList();

            var suppliersWithMostProducts = _supplierService.GetAll()
                .Select(s => new
                {
                    Supplier = s,
                    ProductCount = _productService.GetAll().Count(p => p.SupplierID == s.Id)
                })
                .OrderByDescending(s => s.ProductCount)
                .ToList();

            ViewBag.ProductCount = productCount;
            ViewBag.CategoryCount = categoryCount;
            ViewBag.UserCount = usersCount;
            ViewBag.SupplierCount = supplierCount;
            ViewBag.CategoriesWithMostProducts = categoriesWithMostProducts;
            ViewBag.ProductsWithMostStocked = ProductsWithMostStocked; 
            ViewBag.SuppliersWithMostProducts = suppliersWithMostProducts;

            return View();
        }

    }
}
