using Application.UseCases.Clients.Queries;
using Application.UseCases.Clients.Responses;
using Application.UseCases.Clients.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Clients.Filters;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles ="GetClientById")]
    public async ValueTask<ClientResponse> GetClientById(Guid ClientId)
        => await _mediator.Send(new GetByIdClientQuery(ClientId));

    [HttpGet("[action]")]
    [Authorize(Roles ="GetAllClient")]
    public async ValueTask<IEnumerable<GetListClientResponse>> GetAllClient(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListClientResponse> query = (await _mediator
             .Send(new GetAllClientQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="CreateClient")]
    public async ValueTask<Guid> CreateClient(CreateClientCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles ="UpdateClient")]
    public async ValueTask<IActionResult> UpdateClient(UpdateClientCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles ="DeleteClient")]
    public async ValueTask<IActionResult> DeleteClient(DeleteClientCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("[action]")]
    [Authorize(Roles ="GetFilteredClientByBirthDate")]
    public async ValueTask<IEnumerable<GetListClientResponse>> GetFilteredClientByBirthDate(DateOnly MinDate, DateOnly? MaxDate = null, int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListClientResponse> query = (await _mediator
             .Send(new FilterByBirthDate(MinDate, MaxDate)))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }
}
