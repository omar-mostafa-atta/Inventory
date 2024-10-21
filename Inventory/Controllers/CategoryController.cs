using Inventory.DB.ViewModels;
using Inventory.Service.Sevices.CategoryService;
using Inventory.Service.Sevices.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _productService;



        public CategoryController(ICategoryService CategoryService, IProductService productService)
        {
            _CategoryService = CategoryService;
            _productService = productService;
        }
		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpGet]
        public IActionResult Insert()
        {
           
            return View();
        }

		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpPost]
        public IActionResult Insert(CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
              
                var category = new Category
                {
                    CategoryName = viewModel.CategoryName,
                     Description = viewModel.Description,
                     imageurl = viewModel.imageurl

                };

                _CategoryService.Insert(category);
               
                return RedirectToAction(nameof(GetAll));
            }
            return View(viewModel);
        }

        public IActionResult GetAll(string searchString)
        
        {
            var categories = _CategoryService.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(x => x.CategoryName.Contains(searchString)).ToList();
            }

            return View(categories);
        }
		[Authorize]
		[Authorize(Roles = "admin")]
		public IActionResult GetById(int id)
        {
            var category = _CategoryService.GetById(id);
            return View("GetById", category);
        }

		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpGet]
        public IActionResult Update(int id)
        {
            var Category = _CategoryService.GetById(id);
            if (Category == null)
            {
                return NotFound("this Category doesn't exist");
            }
            var viewModel = new CategoryViewModel
            {
                CategoryName = Category.CategoryName,
                Description = Category.Description,
                imageurl = Category.imageurl

            };

            return View(viewModel);
        }
		[Authorize]
		[Authorize(Roles = "admin")]
		[HttpPost]
        public IActionResult Update(int id, CategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _CategoryService.GetById(id);
                if (existingCategory == null)
                {
                    return NotFound("This Category doesn't exist.");
                }

                existingCategory.CategoryName = viewModel.CategoryName;
                existingCategory.Description = viewModel.Description;
                existingCategory.imageurl = viewModel.imageurl;


                _CategoryService.Update(existingCategory);

                return RedirectToAction(nameof(GetAll));
            }
            return View(viewModel);
        }
		[Authorize]
		[Authorize(Roles = "admin")]
		public IActionResult Delete(int id)
        {
            _CategoryService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult GetAllProducts(int id)
        {
            var products = _productService.GetAll();
            var ourProducts = products.Where(p => p.CategoryID == id).ToList();
            return View("CategoryProducts", ourProducts);
        }

    }
}

