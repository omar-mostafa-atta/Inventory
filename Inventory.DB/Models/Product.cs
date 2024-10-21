using Inventory.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }
  
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int StockQuantity { get; set; }
    
    public int LowStockThreshold { get; set; }

    public string imageurl { get; set; }

    public int CategoryID { get; set; }
    public Category Category { get; set; }
   
    public int SupplierID { get; set; }
    public Supplier supplier { get; set; }

    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
