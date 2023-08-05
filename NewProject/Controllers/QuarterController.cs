using Application.UseCases.Quarters.Queries;
using Application.UseCases.Quarters.Responses;
using Application.UseCases.Quarters.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuarterController : ApiBaseController
    {
        [HttpGet("[action]")]
        public async ValueTask<QuarterResponse> GetQuarterById(Guid QuarterId)
       => await _mediator.Send(new GetByIdQuarterQuery(QuarterId));

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<QuarterResponse>> GetAllQuarter(int PageNumber = 1, int PageSize = 10)
        {
            IPagedList<QuarterResponse> query = (await _mediator
                 .Send(new GetAllQuarterQuery()))
                 .ToPagedList(PageNumber, PageSize);
            return query;
        }

        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateQuarter(CreateQuarterCommand command)
            => await _mediator.Send(command);


        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateQuarter(UpdateQuarterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteQuarter(DeleteQuarterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
