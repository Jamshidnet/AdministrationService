using Application.UseCases.Logs.Queries;
using Application.UseCases.Logs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserActionLogController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllUserActionLogs")]
    public async ValueTask<IEnumerable<UserActionResponse>> GetAllUserActionLogs(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<UserActionResponse> query = (await _mediator
             .Send(new GetAllUserActionQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }
}
