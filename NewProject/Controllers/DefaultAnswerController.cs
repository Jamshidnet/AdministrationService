
using Application.UseCases.DefaultAnswers.Commands;
using Application.UseCases.DefaultAnswers.Queries;
using Application.UseCases.DefaultAnswers.Responses;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NewProject.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class DefaultAnswerController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<DefaultAnswerResponse> GetDefaultAnswerById(Guid DefaultAnswerId)
            => await _mediator.Send(new GetByIdDefaultAnswerQuery(DefaultAnswerId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<DefaultAnswerResponse>> GetAllDefaultAnswer(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<DefaultAnswerResponse> query = (await _mediator
             .Send(new GetAllDefaultAnswerQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
        public async ValueTask<Guid> CreateDefaultAnswer(CreateDefaultAnswerCommand command)
            => await _mediator.Send(command);


        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateDefaultAnswer(UpdateDefaultAnswerCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteDefaultAnswer(DeleteDefaultAnswerCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }

