using ERP.StudentRegistration.Core.Contracts;
using MassTransit;

namespace ERP.ModuleRegistration.Api.Services
{
    public class StudentNotificationConsumer(ILogger<StudentCreatedNotification> logger) : IConsumer<StudentCreatedNotification>
    {
        public async Task Consume(ConsumeContext<StudentCreatedNotification> context)
        {
            logger.LogInformation($"Recieved Student created notification{context.Message.StudentId} and {context.Message.RegistrationNumber}");
            await Task.CompletedTask;
        }
    }
  }
