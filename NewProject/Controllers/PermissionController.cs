using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Application.UseCases.Permissions.Queries;
using Application.UseCases.Permissions.Responses;
using Application.UseCases.Permissions.Commands.CreatePermission;
using Application.UseCases.Permissions.Commands.DeletePermission;
using Application.UseCases.Permissions.Commands.UpdatePermission;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ApiBaseController
    {

        [HttpGet("[action]")]
        [Authorize(Roles ="GetPermissionById")]
        public async ValueTask<PermissionResponse> GetPermissionById(Guid Id)
            => await _mediator.Send(new GetByIdPermissionQuery(Id)); 

        [HttpGet("[action]")]
      [Authorize(Roles ="GetAllPermission")]
        public async ValueTask<IEnumerable<PermissionResponse>> GetAllPermission(int PageNumber = 1, int PageSize = 10)
        {
            IPagedList<PermissionResponse> query = (await _mediator
                 .Send(new GetAllPermissionQuery()))
                 .ToPagedList(PageNumber, PageSize);
            return query;
        }
        
        [HttpGet("[action]")]
        public async ValueTask<IEnumerable<PermissionResponse>> GetAllPermissionWithoutPagination()
        {
            List<PermissionResponse> query = await _mediator
                 .Send(new GetAllPermissionQuery());
                 
            return query;
        }


        [HttpPost("[action]")]
       [Authorize(Roles ="CreatePermission")]
        public async ValueTask<List<PermissionResponse>> CreatePermission(CreatePermissionCommand command)
            => await _mediator.Send(command);


        [HttpPut("[action]")]
        [Authorize(Roles ="UpdatePermission")]
        public async ValueTask<IActionResult> UpdatePermission(UpdatePermissionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("[action]")]
        [Authorize(Roles ="DeletePermission")]
        public async ValueTask<IActionResult> DeletePermission(DeletePermissionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
