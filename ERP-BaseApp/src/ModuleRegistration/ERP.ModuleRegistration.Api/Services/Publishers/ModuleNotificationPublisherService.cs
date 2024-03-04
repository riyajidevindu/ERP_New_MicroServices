using ERP.ModuleRegistration.Api.Services.publishers.Interfaces;
using ERP.ModuleRegistration.Core.Contracts;
using MassTransit;
using System.Runtime.CompilerServices;

namespace ERP.ModuleRegistration.Api.Services.publishers
{
    public class ModuleNotificationPublisherService(
        ILogger<ModuleNotificationPublisherService> logger, IBus bus
        ) : IModuleNotificationPublisherService
    {

        public async Task SendNotification(Guid ModuleId, string RegistrationNo)
        {
            logger.LogInformation($"Notification {ModuleId}");
            
            await bus.Publish(new ModuleCreatedNotification(ModuleId, RegistrationNo));
        }
    }
}
