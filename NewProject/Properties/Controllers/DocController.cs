using Application.UseCases.Docs.Queries;
using Application.UseCases.Docs.Responses;
using Application.UseCases.Docs.Commands;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Application.UseCases.Docs.Filters;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class DocController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles ="GetDocById")]
    public async ValueTask<DocResponse> GetDocById(Guid DocId)
   => await _mediator.Send(new GetByIdDocQuery(DocId));

    [HttpGet("[action]")]
    [Authorize(Roles ="GetAllDoc")]
    public async ValueTask<IEnumerable<GetListDocResponse>> GetAllDoc(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListDocResponse> query = (await _mediator
             .Send(new GetAllDocQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="CreateDoc")]
    public async ValueTask<Guid> CreateDoc(CreateDocCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles ="UpdateDoc")]
    public async ValueTask<IActionResult> UpdateDoc(UpdateDocCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles ="DeleteDoc")]
    public async ValueTask<IActionResult> DeleteDoc(DeleteDocCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="GetFilteredDoc")]
    public async ValueTask<IEnumerable<DocCountResponse>> GetFilteredDoc(FilterByDocCount filter, int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<DocCountResponse> query = (await _mediator
             .Send( filter))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="GetFilteredDocsByUserType")]
    public async ValueTask<IEnumerable<FilterByUserResponse>> GetFilteredDocsByUserType(FilterByUser filter, int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<FilterByUserResponse> query = (await _mediator
             .Send(filter))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }
}
