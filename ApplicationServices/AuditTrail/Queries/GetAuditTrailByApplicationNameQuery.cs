using System.Collections.Generic;
using ApplicationServices.Shared.BaseResponse;
using Domain;
using MediatR;

namespace ApplicationServices.AuditTrail.Queries
{
    public class GetAuditTrailByApplicationNameQuery : IRequest<Result<ServiceAuditTrail>>
    {
        public string ApplicationName { get; set; }
    }
}