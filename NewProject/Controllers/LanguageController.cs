using Application.UseCases.Languages.Commands;
using Application.UseCases.Languages.Queries;
using Application.UseCases.Languages.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LanguageController : ApiBaseController
{
 
    [HttpGet("[action]")]
  //  [Authorize(Roles = "GetAllLanguage")]
    public async ValueTask<IEnumerable<LanguageResponse>> GetAllLanguage(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<LanguageResponse> query = (await _mediator
             .Send(new GetAllLanguageQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
//    [Authorize(Roles = "CreateLanguage")]
    public async ValueTask<Guid> CreateLanguage(CreateLanguageCommand command)
        => await _mediator.Send(command);

    [HttpDelete("[action]")]
  //  [Authorize(Roles = "DeleteLanguage")]
    public async ValueTask<IActionResult> DeleteLanguage(DeleteLanguageCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}