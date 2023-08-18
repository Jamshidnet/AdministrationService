using Application.UseCases.Docs.Commands;
using Application.UseCases.Docs.ExportData;
using Application.UseCases.Docs.Filters;
using Application.UseCases.Docs.Queries;
using Application.UseCases.Docs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class DocController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles = "GetDocById")]
    public async ValueTask<DocResponse> GetDocById(Guid DocId)
    {
        return await _mediator.Send(new GetByIdDocQuery(DocId));
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "GetAllDoc")]
    public async ValueTask<IEnumerable<GetListDocResponse>> GetAllDoc(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListDocResponse> query = (await _mediator
             .Send(new GetAllDocQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "CreateDoc")]
    public async ValueTask<Guid> CreateDoc(CreateDocCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("[action]")]
    [Authorize(Roles ="UpdateDoc")]
    public async ValueTask<IActionResult> UpdateDoc(UpdateDocCommand command)
    {
        _ = await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "DeleteDoc")]
    public async ValueTask<IActionResult> DeleteDoc(DeleteDocCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "GetFilteredDoc")]
    public async ValueTask<IEnumerable<DocCountResponse>> GetFilteredDoc(FilterByDocCount filter)
    {
        IEnumerable<DocCountResponse> query = await _mediator
             .Send(filter);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "GetFilteredDocsByUserType")]
    public async ValueTask<IEnumerable<FilterByUserResponse>> GetFilteredDocsByUserType(FilterByUser filter)
    {
        IEnumerable<FilterByUserResponse> query = await _mediator.Send(filter);
        return query;
    }

    [HttpGet("[action]")]
    //[Authorize(Roles = "PDFGetDocById")]
    public async ValueTask<FileResult> PDFGetDocById(Guid DocId, string filename = "DocFile")
    {
        var result = await _mediator.Send(new GetDocPDF(filename, DocId));
        return File(result.FileContents, result.Options, result.FileName);
    }

    [HttpGet("[action]")]
   // [Authorize(Roles = "ExportExcelDocs")]
    public async Task<FileResult> ExportExcelDocs(string fileName = "DocsFile")
    {
        var result = await _mediator.Send(new GetDocsExcelQuery(fileName));
        return File(result.FileContents, result.Option, result.FileName);
    }
}

