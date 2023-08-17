using Application.UseCases.ClientAnswers.Commands;
using Application.UseCases.DefaultAnswers.Commands;
using Application.UseCases.DefaultAnswers.Queries;
using Application.UseCases.DefaultAnswers.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientAnswerController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetClientAnswerById")]
    public async ValueTask<ClientAnswerResponse> GetClientAnswerById(Guid ClientAnswerId)
    {
        return await _mediator.Send(new GetByIdClientAnswerQuery(ClientAnswerId));
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllClientAnswer")]
    public async ValueTask<IEnumerable<ClientAnswerResponse>> GetAllClientAnswer(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<ClientAnswerResponse> query = (await _mediator
             .Send(new GetAllClientAnswerQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "CreateClientAnswer")]
    public async ValueTask<Guid> CreateClientAnswer(CreateClientAnswerCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "UpdateClientAnswer")]
    public async ValueTask<IActionResult> UpdateClientAnswer(UpdateClientAnswerCommand command)
    {
        _ = await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "DeleteClientAnswer")]
    public async ValueTask<IActionResult> DeleteClientAnswer(DeleteClientAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
