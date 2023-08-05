using Application.UseCases.ClientTypes.Queries;
using Application.UseCases.ClientTypes.Responses;
using Application.UseCases.ClientTypes.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ClientTypeController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<ClientTypeResponse> GetClientTypeById(Guid ClientTypeId)
   => await _mediator.Send(new GetByIdClientTypeQuery(ClientTypeId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<ClientTypeResponse>> GetAllClientType(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<ClientTypeResponse> query = (await _mediator
             .Send(new GetAllClientTypeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateClientType(CreateClientTypeCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateClientType(UpdateClientTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteClientType(DeleteClientTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
