using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using X.PagedList;
using Application.UseCases.UserTypes.Responses;
using Application.UseCases.UserTypes.Queries;
using Application.UseCases.UserTypes.Commands;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTypeController : ApiBaseController
{

    [HttpGet("[action]")]
    public async ValueTask<UserTypeResponse> GetUserTypeById(Guid UserId)
   => await _mediator.Send(new GetByIdUserTypeQuery(UserId));

    
    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<UserTypeResponse>> GetAllUser (int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<UserTypeResponse> query = (await _mediator
             .Send(new GetAllUserTypeQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateUser(CreateUserTypeCommand command)
        => await _mediator.Send(command);


    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateUser(UpdateUserTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteUser(DeleteUserTypeCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }


}
