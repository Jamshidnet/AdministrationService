using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.QuestionTypes.Queries;
using Application.UseCases.QuestionTypes.Responses;
using Application.UseCases.QuestionTypes.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class QuestionTypeController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<QuestionTypeResponse> GetQuestionTypeById(Guid QuestionTypeId)
   => await _mediator.Send(new GetByIdQuestionTypeQuery(QuestionTypeId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<QuestionTypeResponse>> GetAllQuestionType(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<QuestionTypeResponse> query = (await _mediator
             .Send(new GetAllQuestionTypeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateQuestionType(CreateQuestionTypeCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateQuestionType(UpdateQuestionTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteQuestionType(DeleteQuestionTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
