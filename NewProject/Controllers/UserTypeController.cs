using Application.UseCases.UserTypes.Commands;
using Application.UseCases.UserTypes.Queries;
using Application.UseCases.UserTypes.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTypeController : ApiBaseController
{
    [Authorize(Roles = "GetUserTypeById")]
    [HttpGet("[action]")]
    public async ValueTask<UserTypeResponse> GetUserTypeById(Guid UserId)
   => await _mediator.Send(new GetByIdUserTypeQuery(UserId));


    [Authorize(Roles = "GetAllUserType")]
    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<GetListUserTypeResponse>> GetAllUserType(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<GetListUserTypeResponse> query = (await _mediator
             .Send(new GetAllUserTypeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [Authorize(Roles = "CreateUserType")]
    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateUserType(CreateUserTypeCommand command)
        => await _mediator.Send(command);


    [Authorize(Roles = "UpdateUserType")]
    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateUserType(UpdateUserTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "DeleteUserType")]
    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteUserType(DeleteUserTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }


}
