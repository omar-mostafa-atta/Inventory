using Inventory.DB.ViewModels;
using Inventory.Service.Sevices.AlertService;
using Inventory.Service.Sevices.ProductService;
using Inventory.Service.Sevices.TransactionService;
using Inventory.Service.Sevices.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Controllers
{
	[Authorize]
	[Authorize(Roles = "admin")]
	public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IAlertService _alertService;


        public TransactionController(ITransactionService transactionService, IUserService userService, IProductService productService, IAlertService alertService)
        {
            _transactionService = transactionService;
            _userService = userService;
            _productService = productService;
            _alertService = alertService;
        }

        public IActionResult GetAll(string searchString)
        {
            var Transactions = _transactionService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                Transactions = Transactions.Where(x => x.TransactionType.Contains(searchString)).ToList();
            }

            foreach (var transaction in Transactions)
            {
                transaction.Product = _productService.GetById(transaction.ProductID);
            }
            return View("GetAll", Transactions);
        }


        public IActionResult GetById(int id)
        {
            var Transactions = _transactionService.GetById(id);
            return View("GetById", Transactions);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var products = _productService.GetAll();
            var users = _userService.GetAll();

            
            if (products == null || users == null)
            {
                return NotFound("Products or Users data is not available.");
            }

            
            var selectProductItems = products.Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name,
            }).ToList();

            var selectUserItems = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.UserName,
            }).ToList();

            
            var viewModel = new TransactionViewModel
            {
                products = selectProductItems,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TransactionViewModel vm)
        {

            var product = _productService.GetById(vm.ProductID);

            if (product == null)
            {
                ModelState.AddModelError("", "Product not found.");
                return View(vm);
            }


            if (vm.TransactionType == "Withdraw")
            {

                if (product.StockQuantity < vm.Quantity)
                {
                    ModelState.AddModelError("", "Not enough stock available for withdrawal.");
                    return View(vm);
                }


                product.StockQuantity -= vm.Quantity;
                if (product.StockQuantity < product.LowStockThreshold)
                {
                    Alert enzar = MakeAlert(product.ID);
                    _alertService.Insert(enzar);
                }
            }
            else if (vm.TransactionType == "Add")
            {

                product.StockQuantity += vm.Quantity;
            }


            _productService.Update(product);


            var transaction = new Transaction
            {
                TransactionType = vm.TransactionType,
                Quantity = vm.Quantity,
                TransactionDate = vm.TransactionDate,
                ProductID = vm.ProductID,
            };


            _transactionService.Insert(transaction);

            return RedirectToAction(nameof(GetAll));
        }

        public Alert MakeAlert(int productID)
        {
            Alert ourAlert = new Alert();
            ourAlert.ProductID = productID;
            ourAlert.Product = _productService.GetById(productID);
            if (ourAlert.Product == null)
            {
                throw new Exception($"Product with ID {productID} not found.");
            }
            ourAlert.AlertDate = DateTime.Now;
            ourAlert.IsResolved = false;
            ourAlert.Description = $"The {ourAlert.Product.Name} Item is low, with {ourAlert.Product.StockQuantity} in the stock ";
            return ourAlert;
        }
    }
}
