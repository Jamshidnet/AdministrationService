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
public class DocChangeLogController : ApiBaseController
{

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllDocChangeLogs")]
    public async ValueTask<IEnumerable<DocChangeResponse>> GetAllDocChangeLogs(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<DocChangeResponse> query = (await _mediator
             .Send(new GetAllDocChangeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }
}
