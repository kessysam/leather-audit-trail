using ApplicationServices.Shared.BaseResponse;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApplicationServices.AuditTrail.Command
{
    public class CreateAuditTrailCommand : IRequest<Result>
    {
        public string ApplicationName { get; set; }
    }
}
