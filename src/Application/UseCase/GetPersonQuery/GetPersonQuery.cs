using Domain.Common.Models;
using MediatR;

namespace Application.UseCase.GetPersonQuery;

public class GetPersonQuery : IRequest<ServiceResult>
{
    public int Id { get; set; }
}