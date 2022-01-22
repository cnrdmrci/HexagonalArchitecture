using Application.UseCase.CreatePersonCommand;
using Application.UseCase.GetPersonQuery;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.v1;

[ApiVersion("1.0")]
public class PersonController : BaseApiController
{
    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetPerson(int id)
    {
        var query = new GetPersonQuery { Id = id };
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}