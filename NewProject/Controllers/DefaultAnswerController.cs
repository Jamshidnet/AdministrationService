
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
public class DefaultAnswerController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetDefaultAnswerById")]
    public async ValueTask<DefaultAnswerResponse> GetDefaultAnswerById(Guid DefaultAnswerId)
        => await _mediator.Send(new GetByIdDefaultAnswerQuery(DefaultAnswerId));

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllDefaultAnswer")]
    public async ValueTask<IEnumerable<DefaultAnswerResponse>> GetAllDefaultAnswer(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<DefaultAnswerResponse> query = (await _mediator
             .Send(new GetAllDefaultAnswerQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "CreateDefaultAnswer")]
    public async ValueTask<Guid> CreateDefaultAnswer(CreateDefaultAnswerCommand command)
            => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles = "UpdateDefaultAnswer")]
    public async ValueTask<IActionResult> UpdateDefaultAnswer(UpdateDefaultAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "DeleteDefaultAnswer")]
    public async ValueTask<IActionResult> DeleteDefaultAnswer(DeleteDefaultAnswerCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}

