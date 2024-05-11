namespace ThunderWingsECommerceChallenge.Models;

public class Aircraft
{
    public int Id { get; set; } 
    public string Name { get; private set; }
    public string Manufacturer { get; private set; }
    public string Country { get; private set; }
    public string Role { get; private set; }
    public int TopSpeed { get; private set; }
    public decimal Price { get; private set; }

    public Aircraft(
        string name, 
        string manufacturer, 
        string country, 
        string role, 
        int topSpeed, 
        decimal price)
    {
        this.Name = name;
        this.Manufacturer = manufacturer;
        this.Country = country;
        this.Role = role;
        this.TopSpeed = topSpeed;
        this.Price = price;
    }

    public void UpdateAircraft(string name,
        string manufacturer,
        string country,
        string role,
        int topSpeed,
        decimal price)
    {
        this.Name = name;
        this.Manufacturer = manufacturer;
        this.Country = country;
        this.Role = role;
        this.TopSpeed = topSpeed;
        this.Price = price;
    }
}