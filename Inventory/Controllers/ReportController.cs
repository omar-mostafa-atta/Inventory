using Inventory.Service.Sevices.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
	[Authorize]
	[Authorize(Roles = "admin")]
	public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            var combinedReport = _reportService.GenerateCombinedReport();
            return View(combinedReport);
        }
    }
}
