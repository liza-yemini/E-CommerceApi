namespace VattaAppApi.Models;

public class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public int Apartment { get; set; }
    public int Floor { get; set; }
    public int PostalCode { get; set; }
    public int State { get; set; }
    public int PhoneNumber { get; set; }

}