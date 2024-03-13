using ERP.Messaging.Core.Student;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Messaging.Core.Consumers
{
    public class StudentCreatedNotificationConsumer : IConsumer<StudentCreatedNotificationRecord>
    {
        private readonly ILogger<StudentCreatedNotificationConsumer> _logger;

        public StudentCreatedNotificationConsumer(ILogger<StudentCreatedNotificationConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<StudentCreatedNotificationRecord> context)
        {
            _logger.LogInformation($"Authentication Log : Recieved {context.Message.StudentFullName}");
            return Task.CompletedTask;
        }
    }
}
