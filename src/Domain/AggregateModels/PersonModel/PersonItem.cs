using Domain.Common.Abstract;

namespace Domain.AggregateModels.PersonModel;

public class PersonItem : BaseEntity
{
    public string ItemName { get; private set; }

    public PersonItem()
    {
    }

    public PersonItem(string itemName)
    {
        ItemName = itemName;
    }
}