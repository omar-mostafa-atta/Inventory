using System.ComponentModel.DataAnnotations;

public class Transaction
{   
 
    public int ID { get; set; }

    public string TransactionType { get; set; }
    public int Quantity { get; set; }

    public DateTime TransactionDate { get; set; }

    public int ProductID { get; set; }
    public Product Product { get; set; }
}
