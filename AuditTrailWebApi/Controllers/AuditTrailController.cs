using System.Net;
using System.Threading.Tasks;
using ApplicationServices.AuditTrail.Queries;
using ApplicationServices.Shared.BaseResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AuditTrailWebApi.Controllers
{
    public class AuditTrailController : BaseController
    {
        public AuditTrailController(IMediator mediator) : base(mediator)
        {
        }
        
        /// <summary>
        /// retrieves audit trail by application name
        /// </summary>
        /// <param name="query"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/ Customer invoices by customer-id
        ///     "customerId": "00000000-0000-0000-0000-000000000000"
        /// </remarks>
        /// <returns></returns>
        [HttpGet("get-audit-trail-by-application-name")]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Summary = "retrieves audit trails by application name")]
        public async Task<IActionResult> GetAuditTrailByApplicationNameAsync([FromQuery] GetAuditTrailByApplicationNameQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        /// <summary>
        /// retrieves audit trail by application name
        /// </summary>
        /// <param name="query"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/ Customer invoices by customer-id
        ///     "customerId": "00000000-0000-0000-0000-000000000000"
        /// </remarks>
        /// <returns></returns>
        [HttpGet("get-audit-trails")]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Result), (int)HttpStatusCode.BadRequest)]
        [SwaggerOperation(Summary = "retrieves audit trails")]
        public async Task<IActionResult> GetAuditTrailsAsync([FromQuery] GetAuditTrailsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}