using Application.UseCases.Clients.Queries;
using Application.UseCases.Clients.Responses;
using Application.UseCases.Clients.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Clients.Filters;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<ClientResponse> GetClientById(Guid ClientId)
        => await _mediator.Send(new GetByIdClientQuery(ClientId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<ClientResponse>> GetAllClient(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<ClientResponse> query = (await _mediator
             .Send(new GetAllClientQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateClient(CreateClientCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateClient(UpdateClientCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteClient(DeleteClientCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<ClientResponse>> GetFilteredClientByBirthDate(DateOnly MinDate, DateOnly? MaxDate = null, int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<ClientResponse> query = (await _mediator
             .Send(new FilterByBirthDate(MinDate, MaxDate)))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }
}
