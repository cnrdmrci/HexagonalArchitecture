using Domain.Common.Models;
using MediatR;

namespace Application.UseCase.ExceptionTestQuery;

public class GetExceptionTestQueryHandler() : IRequestHandler<GetExceptionTestQuery, ServiceResult>
{
    public Task<ServiceResult> Handle(GetExceptionTestQuery request, CancellationToken cancellationToken)
    {
        throw new DivideByZeroException("value cannot be divided by zero");
    }
}