using Domain.Common.Models;
using MediatR;

namespace Application.UseCase.CreatePersonCommand;

public class CreatePersonCommand : IRequest<ServiceResult>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}