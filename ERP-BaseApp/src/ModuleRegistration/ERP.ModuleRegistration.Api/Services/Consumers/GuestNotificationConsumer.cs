using ERP.ModuleRegistration.Core.Contracts;
using MassTransit;

namespace ERP.ModuleRegistration.Api.Services
{
    public class GuestNotificationConsumer(ILogger<ModuleCreatedNotification> logger) : IConsumer<ModuleCreatedNotification>
    {
        public async Task Consume(ConsumeContext<ModuleCreatedNotification> context)
        {
            logger.LogInformation($"Recieved Student created notification{context.Message.StudentId} and {context.Message.RegistrationNumber}");
            await Task.CompletedTask;
        }
    }
  }
