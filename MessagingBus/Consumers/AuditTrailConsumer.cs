using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.AuditTrail.Command;
using Domain;
using LeatherbackSharedLibrary.AuditTrailMessage;
using LeatherbackSharedLibrary.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MessagingBus.Consumers
{
    public class AuditTrailConsumer : IConsumer<GenericMessage>
    {
        private readonly ILogger<AuditTrailConsumer> _logger;
        private readonly IMediator _mediator;

        public AuditTrailConsumer(ILogger<AuditTrailConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<GenericMessage> context)
        {
            _logger.LogInformation("Gotten message Audit Trail for message ");
            try
            {
                //send to the command processor
                var auditTrailMessages = JsonConvert.DeserializeObject<List<AuditTrailMessage>>(context.Message.Message);
                var serviceAuditTrails = auditTrailMessages.Select(auditTrailMessage => new ServiceAuditTrail
                    {
                        ApplicationName = auditTrailMessage.ApplicationName,
                        AuditType = auditTrailMessage.Type,
                        PrimaryKey = auditTrailMessage.PrimaryKey,
                        UserId = auditTrailMessage.UserId,
                        NewValues = auditTrailMessage.NewValues,
                        AffectedColumns = auditTrailMessage.AffectedColumns,
                        TableName = auditTrailMessage.TableName,
                        OldValues = auditTrailMessage.OldValues,
                        DateTime = auditTrailMessage.DateTime,
                        Id = Guid.NewGuid().ToString()
                    })
                    .ToList();
                var auditTrailCommand = new CreateAuditTrailCommand
                {
                    ServiceAuditTrails = serviceAuditTrails
                };
                var result = await _mediator.Send(auditTrailCommand);
                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Audit trail not saved...Details: {JsonConvert.SerializeObject(serviceAuditTrails, Formatting.None)}");
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