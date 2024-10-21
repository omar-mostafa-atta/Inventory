using Inventory.DB.ViewModels;
using Inventory.DB.ViewModels.Report;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Repositories;

namespace Inventory.Service.Sevices.Reports
{
	public class ReportService : IReportService
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IGenericRepository<Supplier> _supplierRepository;

		public ReportService(IGenericRepository<Product> productRepository, IGenericRepository<Supplier> supplierRepository)
		{
			_productRepository = productRepository;
			_supplierRepository = supplierRepository;
		}

		public CombinedReportViewModel GenerateCombinedReport()

		{
			var combinedReport = new CombinedReportViewModel
			{
				StockReports = _productRepository.GetAll()
					.Select(prod => new StockReportViewModel
					{
						ProductName = prod.Name,
						StockQuantity = prod.StockQuantity,
						LowStockThreshold = prod.LowStockThreshold
					}).ToList(),

				ProductReports = _productRepository.GetAllQueryable()
				.Include(p => p.Category)
				.Include(p => p.supplier)
				.Select(prod => new ProductReportViewModel
				{
					ProductName = prod.Name,
					Category = prod.Category.CategoryName,
					Supplier = prod.supplier.Name,
					Price = prod.Price,
					StockQuantity = prod.StockQuantity,
					LowStockStatus = GetLowStockStatus(prod.StockQuantity, prod.LowStockThreshold)
				}).ToList(),

				SupplierReports = _supplierRepository.GetAll()
				.Select(sup => new SupplierReportViewModel
				{
					SupplierName = sup.Name,
					Email = sup.Email,
					Phone = sup.Phone,
					ProductCount = sup.Products != null ? sup.Products.Count() : 0, 
					ProductNames = sup.Products != null ? sup.Products.Select(p => p.Name).ToList() : new List<string>()
				}).ToList()};
			return combinedReport;
		}
		private static string GetLowStockStatus(int stockQuantity, int lowStockThreshold)
		{

			if (lowStockThreshold < 0)
			{
				return "Invalid threshold";
			}
			else if (stockQuantity == lowStockThreshold + 1 || stockQuantity == lowStockThreshold + 2)
			{
				return "About to run out";
			}

			if (stockQuantity >= lowStockThreshold)
			{
				return "Accepted";
			}

			else
			{
				return "Not Accepted";
			}
		}
	}
}