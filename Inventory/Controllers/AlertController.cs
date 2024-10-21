using Inventory.DB.ViewModels;
using Inventory.Service.Sevices.AlertService;
using Inventory.Service.Sevices.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Controllers
{
	[Authorize]
	[Authorize(Roles = "admin")]
	public class AlertController : Controller
    {
        private readonly IAlertService _alertService;
        private readonly IProductService _productService;


        public AlertController(IAlertService alertService, IProductService productService)
        {
            _alertService = alertService;
            _productService = productService;
        }

        public IActionResult GetAll()
        {
            var alerts = _alertService.GetAll();
            return View("GetAll", alerts);
        }
        public IActionResult GetById(int id)
        {
            var alert = _alertService.GetById(id);
            return View("GetById", alert);
        }
        public IActionResult Delete(int id)
        {
            _alertService.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }
    }
}
