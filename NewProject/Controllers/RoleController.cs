using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Roles.Responses;
using Application.UseCases.Roles.Commands;
using Application.UseCases.Roles.Qeuries;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiBaseController
    {

        [HttpGet("[action]")]
        public async ValueTask<RoleResponse> GetRoleById(Guid Id)
            => await _mediator.Send(new GetByIdRoleQuery(Id));

        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<RoleResponse>> GetAllRole(int PageNumber = 1, int PageSize = 10)
        {
            IPagedList<RoleResponse> query = (await _mediator
                 .Send(new GetAllRoleQuery()))
                 .ToPagedList(PageNumber, PageSize);
            return query;
        }

        [HttpPost("[action]")]
        public async ValueTask<Guid> CreateRole(CreateRoleCommand command)
            => await _mediator.Send(command);

        [HttpPut("[action]")]
        public async ValueTask<IActionResult> UpdateRole(UpdateRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        public async ValueTask<IActionResult> DeleteRole(DeleteRoleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
