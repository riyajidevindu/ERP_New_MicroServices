using ERP.LabEquipmentManagement.Api.Services.Interfaces;
using ERP.LabEquipmentManagement.Core.Contracts;
using MassTransit;

namespace ERP.LabEquipmentManagement.Api.Services
{
    public class LabEquipmentNotificationPublisherService : ILabEquipmentNotificationPublisherService
    {
        private readonly ILogger<LabEquipmentNotificationPublisherService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public LabEquipmentNotificationPublisherService(
            ILogger<LabEquipmentNotificationPublisherService> logger,
             IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task SentNotification(Guid labEquipmentId, string labEquipmentName)
        {
            _logger.LogInformation($"Lab Equipment Notification for {labEquipmentId}");

            await _publishEndpoint.Publish(new LabEquipmentNotificationRecord(labEquipmentId, labEquipmentName));
        }
    }
}
