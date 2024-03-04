using ERP.StudentRegistration.Api.Services.Publishers.Interfaces;
using ERP.StudentRegistration.Core.Contracts;
using MassTransit;
using System.Runtime.CompilerServices;

namespace ERP.StudentRegistration.Api.Services.Publishers
{
    public class StudentNotificationPublisherService(
        ILogger<StudentNotificationPublisherService> logger, IBus bus
        ) : IStudentNotificationPublisherService
    {

        public async Task SendNotification(Guid StudentId, string RegistrationNo)
        {
            logger.LogInformation($"Notification {StudentId}");
            await bus.Publish(new StudentCreatedNotification(StudentId, RegistrationNo));
        }
    }
}
