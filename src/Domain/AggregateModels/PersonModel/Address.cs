using Domain.Common.Abstract;

namespace Domain.AggregateModels.PersonModel;

public class Address : ValueObject
{
    public int Id { get; set; }
    public string City { get; private set; }
    public string Country { get; private set; }

    public Address()
    {
    }

    public Address(string city, string country)
    {
        City = city;
        Country = country;
    }

    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Country;
    }
}