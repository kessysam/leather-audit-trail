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

        public CreateAuditTrailCommandHandler(IAuditTrailDbContext context, 
            ILogger<CreateAuditTrailCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Result> Handle(CreateAuditTrailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Saving record for");
            switch (request.ApplicationName)
            {
                //case "Onboarding":
                //    break;
                //case "Transaction":
                //    var auditTrail = new Domain.TransactionAuditTrail();
                //    {

                //    };
                //    _context.TransactionAuditTrails.Add(auditTrail);
                    //break;
            }

            //await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
