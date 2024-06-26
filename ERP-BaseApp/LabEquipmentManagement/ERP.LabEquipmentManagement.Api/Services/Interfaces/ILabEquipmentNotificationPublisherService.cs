namespace ERP.LabEquipmentManagement.Api.Services.Interfaces
{
    public interface ILabEquipmentNotificationPublisherService
    {
        Task SentNotification(Guid LabEquipmentId,string LabEquipmentName);
    }
}
