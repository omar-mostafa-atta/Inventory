using System.ComponentModel.DataAnnotations;

public class Alert
{
    public int ID { get; set; }
    public DateTime AlertDate { get; set; }
    public string Description { get; set; }

    public bool IsResolved { get; set; }

    public int ProductID { get; set; }
    public Product Product  { get; set; }
}
