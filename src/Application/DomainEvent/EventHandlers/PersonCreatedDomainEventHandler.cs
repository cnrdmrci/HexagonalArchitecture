using Application.DomainEvent.Events;
using MediatR;

namespace Application.DomainEvent.EventHandlers;

public class PersonCreatedDomainEventHandler : INotificationHandler<PersonCreatedDomainEvent>
{
    public PersonCreatedDomainEventHandler()
    {
        
    }

    public Task Handle(PersonCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("PersonCreatedDomainEvent published; { Username:" + notification.Username + " }");
        return Task.CompletedTask;
    }
}