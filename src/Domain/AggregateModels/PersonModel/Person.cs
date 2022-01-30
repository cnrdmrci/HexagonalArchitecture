using Domain.Common.Abstract;

namespace Domain.AggregateModels.PersonModel;

public class Person : BaseEntity, IAggregateRoot
{
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public int Age { get; private set; }
    public Address Address { get; private set; }
    public ICollection<PersonItem> PersonItems { get; private set; }

    public Person()
    {
    }
    
    public Person(string username, string name, string surname, int age, Address address, ICollection<PersonItem> personItems)
    {
        Username = username;
        Name = name;
        Surname = surname;
        Age = age;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        PersonItems = personItems ?? throw new ArgumentNullException(nameof(personItems));
    }

    public void AddPersonItem(string personItem)
    {
        PersonItem item = new(personItem);

        PersonItems.Add(item);
    }
}