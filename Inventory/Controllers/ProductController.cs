using Inventory.DB.ViewModels;
using Inventory.Models;
using Inventory.Service.Sevices.CategoryService;
using Inventory.Service.Sevices.ProductService;
using Inventory.Service.Sevices.SupplierService;
using Inventory.Service.Sevices.TransactionService;
using Inventory.Service.Sevices.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Inventory.Controllers
{

	public class ProductController : Controller
	{

		private readonly IProductService _productService;
		private readonly ICategoryService _CategoryService;
		private readonly ISupplierService _SupplierService;
		private readonly IUserService _UserService;
		private readonly ITransactionService _transactionService;

		public ProductController(IProductService productService, ICategoryService CategoryService, ISupplierService SupplierService, IUserService UserService, ITransactionService transactionService)
		{
			_productService = productService;
			_SupplierService = SupplierService;
			_CategoryService = CategoryService;
			_UserService = UserService;
			_transactionService = transactionService;
		}
		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpGet]

		public IActionResult Insert()
		{
			var categories = _CategoryService.GetAll();
			var Suppliers = _SupplierService.GetAll();
			var Users = _UserService.GetAll();

			var selectCategoryItems = categories.Select(c => new SelectListItem
			{
				Value = c.ID.ToString(),
				Text = c.CategoryName,
			}).ToList();
			var selecSupplierItem = Suppliers.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name,
			}).ToList();
			var selectUserItem = Users.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.UserName,
			}).ToList();

			var viewModel = new ProductViewModel
			{
				categories = selectCategoryItems,
				Suppliers = selecSupplierItem,
			};

			return View(viewModel);
		}
		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<IActionResult> Insert(ProductViewModel vm, IFormFile Photo)
		{
            var product = new Product
			{
				Name = vm.Name,
				Price = vm.Price,
				Description = vm.Description,
				CategoryID = vm.CategoryID,
				SupplierID = vm.SupplierID,
				StockQuantity = vm.StockQuantity,
				LowStockThreshold = vm.LowStockThreshold,
				CreatedAt = vm.CreatedAt,
				UpdatedAt = vm.UpdatedAt,
				imageurl = vm.imageurl
			};

            if (product.StockQuantity <= 0)
            {
                ModelState.AddModelError("", "Can not be 0 or low.");
                return View(vm);
            }
            _productService.Add(product);


			var transaction = new Transaction
			{
				TransactionType = "Add New Product",
				TransactionDate = DateTime.Now,
				ProductID = product.ID,
				Quantity = vm.StockQuantity,

			};
			

			_transactionService.Add(transaction);


			return RedirectToAction("GetAll", "Product");
		}

		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpGet]
		public IActionResult Update(int id)
		{
			var Product = _productService.GetById(id);
			if (Product == null)
			{
				return NotFound("this Product doesn't exist");
			}
			var categories = _CategoryService.GetAll();
			var Suppliers = _SupplierService.GetAll();
			var Users = _UserService.GetAll();

			var selectCategoryItems = categories.Select(c => new SelectListItem
			{
				Value = c.ID.ToString(),
				Text = c.CategoryName,
				Selected = c.ID == Product.CategoryID
			}).ToList();
			var selecSupplierItem = Suppliers.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name,
				Selected = c.Id == Product.SupplierID

			}).ToList();
			var selectUserItem = Users.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.UserName,

			}).ToList();



			var viewModel = new ProductViewModel
			{
				Name = Product.Name,
				Description = Product.Description,
				Price = Product.Price,
				StockQuantity = Product.StockQuantity,
				LowStockThreshold = Product.LowStockThreshold,
				CategoryID = Product.CategoryID,
				SupplierID = Product.SupplierID,
				categories = selectCategoryItems,
				Suppliers = selecSupplierItem,
				imageurl = Product.imageurl
			};

			return View(viewModel);
		}
		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpPost]
		public IActionResult Update(int id, ProductViewModel viewModel)
		{
			var existingProduct = _productService.GetById(id);
			if (existingProduct == null)
			{
				return NotFound("This Product doesn't exist.");
			}

			existingProduct.Name = viewModel.Name;
			existingProduct.Description = viewModel.Description;
			existingProduct.Price = viewModel.Price;
			existingProduct.CreatedAt = viewModel.CreatedAt;
			existingProduct.UpdatedAt = viewModel.UpdatedAt;
			existingProduct.StockQuantity = viewModel.StockQuantity;
			existingProduct.LowStockThreshold = viewModel.LowStockThreshold;
			existingProduct.CategoryID = viewModel.CategoryID;
			existingProduct.SupplierID = viewModel.SupplierID;
			existingProduct.imageurl = viewModel.imageurl;


			_productService.Update(existingProduct);

			return RedirectToAction(nameof(GetAll));
		}
		public IActionResult GetAll(string searchString)
		{
			var products = _productService.GetAll();
			if (!String.IsNullOrEmpty(searchString))
			{
				products = products.Where(x => x.Name.Contains(searchString)).ToList();
			}
			return View("GetAll", products);
        }
        
        public IActionResult GetById(int id)
		{
			var product = _productService.GetById(id);

			return View("GetById", product);
		}

		[Authorize]
		[Authorize(Roles = "admin")]
		public IActionResult Delete(int id)
		{
			_productService.Delete(id);
			return RedirectToAction(nameof(GetAll));
		}
	}
}