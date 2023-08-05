using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Regions.Queries;
using Application.UseCases.Regions.Responses;
using Application.UseCases.Regions.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController : ApiBaseController
{
    [HttpGet("[action]")]
  [Authorize(Roles = "GetRegionById")]

    public async ValueTask<RegionResponse> GetRegionById(Guid RegionId)
        => await _mediator.Send(new GetByIdRegionQuery(RegionId));

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllRegion")]
    public async ValueTask<IEnumerable<RegionResponse>> GetAllRegion(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<RegionResponse> query = (await _mediator
             .Send(new GetAllRegionQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    //[Authorize(Roles = "CreateRegion")]
    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateRegion(CreateRegionCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateRegion(UpdateRegionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteRegion(DeleteRegionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
