using Application.UseCases.Regions.Commands;
using Application.UseCases.Regions.Queries;
using Application.UseCases.Regions.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetRegionById")]

    public async ValueTask<RegionResponse> GetRegionById(Guid RegionId)
    {
        return await _mediator.Send(new GetByIdRegionQuery(RegionId));
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllRegion")]
    public async ValueTask<IEnumerable<GetListRegionResponse>> GetAllRegion(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListRegionResponse> query = (await _mediator
             .Send(new GetAllRegionQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [Authorize(Roles = "CreateRegion")]
    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateRegion(CreateRegionCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize(Roles = "UpdateRegion")]
    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateRegion(UpdateRegionCommand command)
    {
         await _mediator.Send(command);
        return NoContent();
    }
    [Authorize(Roles = "DeleteRegion")]
    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteRegion(DeleteRegionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
