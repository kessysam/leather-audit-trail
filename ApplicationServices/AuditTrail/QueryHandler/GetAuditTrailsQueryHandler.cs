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
    public class GetAuditTrailsQueryHandler : IRequestHandler<GetAuditTrailsQuery, Result<IEnumerable<ServiceAuditTrail>>>
    {
        private readonly ILogger<GetAuditTrailsQuery> _logger;
        private readonly IAuditTrailService _auditTrailService;

        public GetAuditTrailsQueryHandler(ILogger<GetAuditTrailsQuery> logger, IAuditTrailService auditTrailService)
        {
            _logger = logger;
            _auditTrailService = auditTrailService;
        }
        
        public async Task<Result<IEnumerable<ServiceAuditTrail>>> Handle(GetAuditTrailsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving multiple audit trails by query string request initiated");

            if (string.IsNullOrEmpty(request.QueryString))
                throw new BadRequestException("Query handler is required.");

            var auditTrails = await _auditTrailService.GetMultipleAsync(request.QueryString);

            return auditTrails == null
                ? Result.Fail(auditTrails, "No audit trails not found.")
                : Result.Ok(auditTrails, "Successful");
        }
    }
}