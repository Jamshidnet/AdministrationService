using Application.UseCases.Districts.Queries;
using Application.UseCases.Districts.Responses;
using Application.UseCases.Districts.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DistrictController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles ="GetDistrictById")]
    public async ValueTask<DistrictResponse> GetDistrictById(Guid DistrictId)
        => await _mediator.Send(new GetByIdDistrictQuery(DistrictId));

    [HttpGet("[action]")]
    [Authorize(Roles ="GetAllDistrict")]
    public async ValueTask<IEnumerable<GetListDIstrictResponse>> GetAllDistrict(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListDIstrictResponse> query = (await _mediator
             .Send(new GetAllDistrictQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="CreateDistrict")]
    public async ValueTask<Guid> CreateDistrict(CreateDistrictCommand command)
        => await _mediator.Send(command);

   [Authorize(Roles = "UpdateDistrict")]
    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateDistrict(UpdateDistrictCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles ="DeleteDistrict")]
    public async ValueTask<IActionResult> DeleteDistrict(DeleteDistrictCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
