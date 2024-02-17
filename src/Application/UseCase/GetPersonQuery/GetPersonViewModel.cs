using Application.Dtos;

namespace Application.UseCase.GetPersonQuery;

public record GetPersonViewModel
{
    public PersonDto PersonDto { get; init; }
}