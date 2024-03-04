namespace ERP.StudentRegistration.Api.Services.Publishers.Interfaces
{
    public interface IStudentNotificationPublisherService
    {
        Task SendNotification(Guid StudentId, string RegistrationNo);
    }
}
