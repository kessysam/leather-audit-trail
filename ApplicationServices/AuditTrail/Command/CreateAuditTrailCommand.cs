using System.Collections.Generic;
using ApplicationServices.Shared.BaseResponse;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApplicationServices.AuditTrail.Command
{
    public class CreateAuditTrailCommand : IRequest<Result>
    {
        public List<ServiceAuditTrail>  ServiceAuditTrails { get; set; }
    }
}
