using Application.UseCases.Quarters.Commands;
using Application.UseCases.Quarters.Queries;
using Application.UseCases.Quarters.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuarterController : ApiBaseController
    {
        [HttpGet("[action]")]
        [Authorize(Roles = "GetQuarterById")]
        public async ValueTask<QuarterResponse> GetQuarterById(Guid QuarterId)
        {
            return await _mediator.Send(new GetByIdQuarterQuery(QuarterId));
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "GetAllQuarter")]
        public async ValueTask<IEnumerable<GetListQuarterResponse>> GetAllQuarter(int PageNumber = 1, int PageSize = 10)
        {
            IPagedList<GetListQuarterResponse> query = (await _mediator
                 .Send(new GetAllQuarterQuery()))
                 .ToPagedList(PageNumber, PageSize);
            return query;
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "CreateQuarter")]
        public async ValueTask<Guid> CreateQuarter(CreateQuarterCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "UpdateQuarter")]
        public async ValueTask<IActionResult> UpdateQuarter(UpdateQuarterCommand command)
        {
            _ = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "DeleteQuarter")]
        public async ValueTask<IActionResult> DeleteQuarter(DeleteQuarterCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
