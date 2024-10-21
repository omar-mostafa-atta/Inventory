using Inventory.DB.ViewModels;
using Inventory.Service.Sevices.ProductService;
using Inventory.Service.Sevices.SupplierService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
	[Authorize]
	[Authorize(Roles = "admin")]
	public class SupplierController : Controller
    {
        private readonly ISupplierService _SupplierService;


        public SupplierController(ISupplierService SupplierService)
        {
            _SupplierService = SupplierService;

        }

        public IActionResult GetAll(string searchString)
        {
            var Suppliers = _SupplierService.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                Suppliers = Suppliers.Where(x => x.Name.Contains(searchString)).ToList();
            }
            return View(Suppliers);
        }
        public IActionResult GetById(int id)
        {
            var Supplier = _SupplierService.GetById(id);
            return View(Supplier);
        }

        [HttpGet]
        public IActionResult Insert()
        {

            return View();
        }

        [HttpPost]

        public IActionResult Insert(SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var supplier = new Supplier
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Phone = viewModel.Phone,
                    imageurl = viewModel.imageurl

                };

                _SupplierService.Insert(supplier);

                return RedirectToAction(nameof(GetAll));
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var Supplier = _SupplierService.GetById(id);
            if (Supplier == null)
            {
                return NotFound("this Supplier doesn't exist");
            }
            var viewModel = new SupplierViewModel
            {
                Name = Supplier.Name,
                Email = Supplier.Email,
                Phone = Supplier.Phone,
                Password = Supplier.Password,
                imageurl = Supplier.imageurl

            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(int id, SupplierViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingSupplier = _SupplierService.GetById(id);
                if (existingSupplier == null)
                {
                    return NotFound("This supplier doesn't exist.");
                }

                existingSupplier.Name = viewModel.Name;
                existingSupplier.Email = viewModel.Email;
                existingSupplier.Phone = viewModel.Phone;
                existingSupplier.imageurl = viewModel.imageurl;

                if (!string.IsNullOrEmpty(viewModel.Password))
                {
                    existingSupplier.Password = viewModel.Password;
                }

                _SupplierService.Update(existingSupplier);

                return RedirectToAction(nameof(GetAll));
            }
            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            _SupplierService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }

    }
}
