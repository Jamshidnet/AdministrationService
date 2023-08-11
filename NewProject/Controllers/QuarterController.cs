using Application.UseCases.Quarters.Queries;
using Application.UseCases.Quarters.Responses;
using Application.UseCases.Quarters.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuarterController : ApiBaseController
    {
        [HttpGet("[action]")]
        [Authorize(Roles ="GetQuarterById")]
        public async ValueTask<QuarterResponse> GetQuarterById(Guid QuarterId)
       => await _mediator.Send(new GetByIdQuarterQuery(QuarterId));

        [HttpGet("[action]")]
        [Authorize(Roles ="GetAllQuarter")]
        public async ValueTask<IEnumerable<GetListQuarterResponse>> GetAllQuarter(int PageNumber = 1, int PageSize = 10)
        {
            IPagedList<GetListQuarterResponse> query = (await _mediator
                 .Send(new GetAllQuarterQuery()))
                 .ToPagedList(PageNumber, PageSize);
            return query;
        }

        [HttpPost("[action]")]
        [Authorize(Roles ="CreateQuarter")]
        public async ValueTask<Guid> CreateQuarter(CreateQuarterCommand command)
            => await _mediator.Send(command);


        [HttpPut("[action]")]
        [Authorize(Roles ="UpdateQuarter")]
        public async ValueTask<IActionResult> UpdateQuarter(UpdateQuarterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        [Authorize(Roles ="DeleteQuarter")]
        public async ValueTask<IActionResult> DeleteQuarter(DeleteQuarterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
