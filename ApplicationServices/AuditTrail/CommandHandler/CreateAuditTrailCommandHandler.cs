using System.Threading;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using ApplicationServices.Shared.BaseResponse;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.AuditTrail.Command
{
    public class CreateAuditTrailCommandHandler : IRequestHandler<CreateAuditTrailCommand, Result>
    {
        private readonly IAuditTrailDbContext _context;
        private readonly ILogger<CreateAuditTrailCommandHandler> _logger;
        private readonly IAuditTrailService _auditTrailService;

        public CreateAuditTrailCommandHandler(IAuditTrailDbContext context, 
            ILogger<CreateAuditTrailCommandHandler> logger, IAuditTrailService auditTrailService)
        {
            _context = context;
            _logger = logger;
            _auditTrailService = auditTrailService;
        }
        public async Task<Result> Handle(CreateAuditTrailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Saving record for");
            foreach (var requestServiceAuditTrail in request.ServiceAuditTrails)
            {
                await _auditTrailService.AddAsync(requestServiceAuditTrail);
            }
            return Result.Ok();
        }
    }
}
