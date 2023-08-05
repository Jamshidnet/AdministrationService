using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using Application.UseCases.Categories.Responses;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<CategoryResponse> GetCategoryById(Guid CategoryId)
   => await _mediator.Send(new GetByIdCategoryQuery(CategoryId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<CategoryResponse>> GetAllCategory(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<CategoryResponse> query = (await _mediator
             .Send(new GetAllCategoryQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateCategory(CreateCategoryCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteCategory(DeleteCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
