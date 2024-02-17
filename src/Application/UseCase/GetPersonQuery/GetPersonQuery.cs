using Domain.Common.Models;
using MediatR;

namespace Application.UseCase.GetPersonQuery;

public record GetPersonQuery : IRequest<ServiceResult>
{
    public int Id { get; init; }
}