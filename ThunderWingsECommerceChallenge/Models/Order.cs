using System.ComponentModel.DataAnnotations;

namespace ThunderWingsECommerceChallenge.Models;

public class Order
{
    [Key]
    public int Id { get; set; } 
    public List<Aircraft> BasketItems { get; set; } 
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }
}
