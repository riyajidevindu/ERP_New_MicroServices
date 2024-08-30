namespace ERP.ModuleRegistration.Api.Services.publishers.Interfaces
{
    public interface IModuleNotificationPublisherService
    {
        Task SendNotification(Guid StudentId, string RegistrationNo);
    }
}
