using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Common;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]s")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}