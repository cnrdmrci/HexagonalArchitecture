using MediatR;

namespace Application.DomainEvent.Events;

public record PersonCreatedDomainEvent : INotification
{
    public string Username { get; init; }
    
}