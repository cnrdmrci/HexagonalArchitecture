using MediatR;

namespace Application.DomainEvent.Events;

public class PersonCreatedDomainEvent : INotification
{
    public string Username { get; set; }
    
}