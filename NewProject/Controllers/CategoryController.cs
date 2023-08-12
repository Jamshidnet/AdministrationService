using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using Application.UseCases.Categories.Responses;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
//[LogControllerActions(TableId =TableIdConst.CategoryTable)]
public class CategoryController : ApiBaseController
{
    [HttpGet("[action]")]
    [Authorize(Roles ="GetCategoryById")]
    public async ValueTask<CategoryResponse> GetCategoryById(Guid CategoryId)
   => await _mediator.Send(new GetByIdCategoryQuery(CategoryId));

    [HttpGet("[action]")]
    [Authorize(Roles ="GetAllCategory")]
    public async ValueTask<IEnumerable<GetListCategoryResponse>> GetAllCategory(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListCategoryResponse> query = (await _mediator
             .Send(new GetAllCategoryQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    [Authorize(Roles ="CreateCategory")]
    public async ValueTask<Guid> CreateCategory(CreateCategoryCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    [Authorize(Roles ="UpdateCategory")]
    public async ValueTask<IActionResult> UpdateCategory(UpdateCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpDelete("[action]")]
    [Authorize(Roles ="DeleteCategory")]
    public async ValueTask<IActionResult> DeleteCategory(DeleteCategoryCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

}
