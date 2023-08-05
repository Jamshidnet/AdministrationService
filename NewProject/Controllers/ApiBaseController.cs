using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NewProject.CustomAttributes;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [GlobaExceptionHandler]
    public class ApiBaseController : ControllerBase
    {
        protected IMediator _mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();

    }
}
