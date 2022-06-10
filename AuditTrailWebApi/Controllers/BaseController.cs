using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuditTrailWebApi.Controllers
{
    /// <summary>
    /// Base Controller for other controllers to inherit from.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IMediator _mediator;
        ///s <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
		
    }
}