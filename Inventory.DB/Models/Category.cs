using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    public int ID { get; set; }
    
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public string imageurl { get; set; }
    public ICollection<Product> Products { get; set; }
}
