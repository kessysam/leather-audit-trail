using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationServices.AuditTrail.Queries;
using ApplicationServices.Interfaces;
using ApplicationServices.Shared.BaseResponse;
using ApplicationServices.Shared.Exceptions;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.AuditTrail.QueryHandler
{
    public class GetAuditTrailByApplicationNameQueryHandler : IRequestHandler<GetAuditTrailByApplicationNameQuery, Result<ServiceAuditTrail>>
    {
        private readonly ILogger<GetAuditTrailByApplicationNameQueryHandler> _logger;
        private readonly IAuditTrailService _auditTrailService;

        public GetAuditTrailByApplicationNameQueryHandler(ILogger<GetAuditTrailByApplicationNameQueryHandler> logger, IAuditTrailService auditTrailService)
        {
            _logger = logger;
            _auditTrailService = auditTrailService;
        }
        
        public async Task<Result<ServiceAuditTrail>> Handle(GetAuditTrailByApplicationNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retreiving audit trails by application name request initiated");

            if (string.IsNullOrEmpty(request.ApplicationName))
                throw new BadRequestException("Application name is required.");

            var auditTrail = await _auditTrailService.GetAsync(request.ApplicationName);

            return auditTrail == null
                ? Result.Fail(auditTrail, "Audit trail not found.")
                : Result.Ok(auditTrail, "Successful");
        }
    }
}