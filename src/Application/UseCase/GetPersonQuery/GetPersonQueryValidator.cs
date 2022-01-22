using FluentValidation;

namespace Application.UseCase.GetPersonQuery;

public class GetPersonQueryValidator : AbstractValidator<GetPersonQuery>
{
    public GetPersonQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(0).WithMessage("Id can not be zero")
            .GreaterThanOrEqualTo(1).WithMessage("Id must be greather than zero");
    }
}