using Application.UseCases.Docs.Queries;
using Application.UseCases.Docs.Responses;
using Application.UseCases.Docs.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Application.UseCases.Docs.Filters;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class DocController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<DocResponse> GetDocById(Guid DocId)
   => await _mediator.Send(new GetByIdDocQuery(DocId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<DocResponse>> GetAllDoc(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<DocResponse> query = (await _mediator
             .Send(new GetAllDocQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateDoc(CreateDocCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateDoc(UpdateDocCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteDoc(DeleteDocCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<DocCountResponse>> GetFilteredDoc(FilterByDocCount filter, int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<DocCountResponse> query = (await _mediator
             .Send( filter))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }


}
