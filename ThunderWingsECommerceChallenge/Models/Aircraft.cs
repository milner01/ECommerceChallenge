namespace ThunderWingsECommerceChallenge.Models;

public class Aircraft
{
    public int Id { get; private set; } 
    public string Name { get; private set; }
    public string Manufacturer { get; private set; }
    public string Country { get; private set; }
    public string Role { get; private set; }
    public int TopSpeed { get; private set; }
    public decimal Price { get; private set; }

    public Aircraft(
        int id,
        string name, 
        string manufacturer, 
        string country, 
        string role, 
        int topSpeed, 
        decimal price)
    {
        this.Id = id;
        this.Name = name;
        this.Manufacturer = manufacturer;
        this.Country = country;
        this.Role = role;
        this.TopSpeed = topSpeed;
        this.Price = price;
    }

    public void UpdateAircraft(Aircraft aircraft)
    {
        this.Name = aircraft.Name;
        this.Manufacturer = aircraft.Manufacturer;
        this.Country = aircraft.Country;
        this.Role = aircraft.Role;
        this.TopSpeed = aircraft.TopSpeed;
        this.Price = aircraft.Price;
    }
}