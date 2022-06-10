using System.Collections.Generic;
using ApplicationServices.Shared.BaseResponse;
using Domain;
using MediatR;

namespace ApplicationServices.AuditTrail.Queries
{
    public class GetAuditTrailsQuery : IRequest<Result<IEnumerable<ServiceAuditTrail>>>
    {
        public string QueryString { get; set; }
    }
}