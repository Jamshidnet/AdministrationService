using Application.UseCases.ClientAnswers.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using X.PagedList;
using Application.UseCases.DefaultAnswers.Responses;
using Application.UseCases.DefaultAnswers.Queries;
using Application.UseCases.DefaultAnswers.Commands;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientAnswerController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<ClientAnswerResponse> GetClientAnswerById(Guid ClientAnswerId)
        => await _mediator.Send(new GetByIdClientAnswerQuery(ClientAnswerId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<ClientAnswerResponse>> GetAllClientAnswer(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<ClientAnswerResponse> query = (await _mediator
             .Send(new GetAllClientAnswerQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateClientAnswer(CreateClientAnswerCommand command)
            => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateClientAnswer(UpdateClientAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteClientAnswer(DeleteClientAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
