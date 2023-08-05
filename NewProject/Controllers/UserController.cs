using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Users.Queries;
using Application.UseCases.Users.Responses;
using Application.UseCases.Users.Commands.CreateUser;
using Application.UseCases.Users.Commands.DeleteUser;
using Application.UseCases.Users.Commands.LoginUser;
using Application.UseCases.Users.Commands.RegesterUser;
using Application.UseCases.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Mvc;
using NewProject.JWT.Models;
using X.PagedList;

namespace NewProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiBaseController
{
    [HttpGet("[action]")]
    public async ValueTask<UserResponse> GetUserById(Guid UserId)
        => await _mediator.Send(new GetByIdUserQuery(UserId));

    [HttpGet("[action]")]
    public async ValueTask<IEnumerable<UserResponse>> GetAllUser(int PageNumber = 1, int PageSize = 10)
    {
        IPagedList<UserResponse> query = (await _mediator
             .Send(new GetAllUserQuery()))
             .ToPagedList(PageNumber, PageSize);
        return query;
    }

    [HttpPost("[action]")]
    public async ValueTask<TokenResponse> RegisterUser(RegisterUserCommand command)
        => await _mediator.Send(command);

    [HttpPost("[action]")]
    public async ValueTask<TokenResponse> LoginUser(LoginUserCommand command)
        => await _mediator.Send(command);

    [HttpPost("[action]")]
    public async ValueTask<Guid> CreateUser(CreateUserCommand command)
        => await _mediator.Send(command);

    [HttpPut("[action]")]
    public async ValueTask<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("[action]")]
    public async ValueTask<IActionResult> DeleteUser(DeleteUserCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    //[HttpGet("[action]")]
    //public async ValueTask<IEnumerable<UserResponse>> GetFilteredUsersByBirthDate(int PageNumber = 1, int PageSize = 10)
    //{
    //    IPagedList<UserResponse> query = (await _mediator
    //         .Send(new FilterUserByBirthDate()))
    //         .ToPagedList(PageNumber, PageSize);
    //    return query;
    //}
}
