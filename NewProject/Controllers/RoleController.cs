using Application.UseCases.Roles.Commands;
using Application.UseCases.Roles.Qeuries;
using Application.UseCases.Roles.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiBaseController
    {
        [Authorize(Roles = "GetRoleById")]
        [HttpGet("[action]")]
        public async ValueTask<RoleResponse> GetRoleById(Guid Id)
            => await _mediator.Send(new GetByIdRoleQuery(Id));

        [Authorize(Roles = "GetAllRole")]
        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<GetListRoleResponse>> GetAllRole(int PageNumber = 1, int PageSize = 10)
        {
            IPagedList<GetListRoleResponse> query = (await _mediator
                 .Send(new GetAllRoleQuery()))
                 .ToPagedList(PageNumber, PageSize);
            return query;
        }

       // [Authorize(Roles = "CreateRole")]
        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateRole(CreateRoleCommand command)
            => await _mediator.Send(command);

        [Authorize(Roles = "UpdateRole")]
        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateRole(UpdateRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "DeleteRole")]
        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteRole(DeleteRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
