using Application.UseCases.ClientTypes.Queries;
using Application.UseCases.ClientTypes.Responses;
using Application.UseCases.ClientTypes.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ClientTypeController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles ="GetClientTypeById")]
    public async ValueTask<ClientTypeResponse> GetClientTypeById(Guid ClientTypeId)
   => await _mediator.Send(new GetByIdClientTypeQuery(ClientTypeId));

    [HttpGet("[action]")]
    [Authorize(Roles ="GetAllClientType")]
    public async ValueTask<IEnumerable<GetListClientTypeResponse>> GetAllClientType(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListClientTypeResponse> query = (await _mediator
             .Send(new GetAllClientTypeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="CreateClientType")]
    public async ValueTask<Guid> CreateClientType(CreateClientTypeCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles ="UpdateClientType")]
    public async ValueTask<IActionResult> UpdateClientType(UpdateClientTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles ="DeleteClientType")]
    public async ValueTask<IActionResult> DeleteClientType(DeleteClientTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
