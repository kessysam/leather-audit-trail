using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationServices.AuditTrail.Command;
using LeatherbackSharedLibrary.AuditTrailMessage;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MessagingBus.Consumers
{
    public class AuditTrailConsumer : IConsumer<List<AuditTrailMessage>>
    {
        private readonly ILogger<AuditTrailConsumer> _logger;
        private readonly IMediator _mediator;

        public AuditTrailConsumer(ILogger<AuditTrailConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<List<AuditTrailMessage>> context)
        {
            _logger.LogInformation("Gotten message Audit Trail for message ");
            try
            {
                //send to the command processor

                var auditTrailCommand = new CreateAuditTrailCommand
                {
                    //ApplicationName = context.Message.ApplicationName
                };
                var result = await _mediator.Send(auditTrailCommand);
                if (result.IsSuccess)
                {

                }
            }
            catch (Exception exception)
            {
                //_logger.LogError(exception, "Error occurred for Message from Service " + 
                //                            context.Message.ApplicationName);
            }
        }
    }
}