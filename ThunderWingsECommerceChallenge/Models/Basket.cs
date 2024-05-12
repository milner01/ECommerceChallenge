using System.ComponentModel.DataAnnotations;

namespace ThunderWingsECommerceChallenge.Models;

public class Basket
{
    [Key]
    public int Id { get; private set; }
    public List<Aircraft> Aircrafts { get; set; }
    public string UserName { get; set; } = "Chris";
}
