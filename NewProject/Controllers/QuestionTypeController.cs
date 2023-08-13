using Application.UseCases.QuestionTypes.Commands;
using Application.UseCases.QuestionTypes.Queries;
using Application.UseCases.QuestionTypes.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class QuestionTypeController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetQuestionTypeById")]
    public async ValueTask<QuestionTypeResponse> GetQuestionTypeById(Guid QuestionTypeId)
   => await _mediator.Send(new GetByIdQuestionTypeQuery(QuestionTypeId));

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllQuestionType")]
    public async ValueTask<IEnumerable<GetLIstQuestionTypeResponse>> GetAllQuestionType(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetLIstQuestionTypeResponse> query = (await _mediator
             .Send(new GetAllQuestionTypeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "CreateQuestionType")]
    public async ValueTask<Guid> CreateQuestionType(CreateQuestionTypeCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles = "UpdateQuestionType")]
    public async ValueTask<IActionResult> UpdateQuestionType(UpdateQuestionTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "DeleteQuestionType")]
    public async ValueTask<IActionResult> DeleteQuestionType(DeleteQuestionTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}

