using ERP.Messaging.Core.Service.Intefeaces;
using ERP.Messaging.Core.Student;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ERP.Messaging.Core.Service
{
    public class StudentCreatedNotificationPublisherService
        : IStudentCreatedNotificationPublisherService
    {
        private readonly ILogger<StudentCreatedNotificationPublisherService> _logger;
        private readonly IBus _bus;

        public StudentCreatedNotificationPublisherService(
            ILogger<StudentCreatedNotificationPublisherService> logger,
            IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }


        public  async Task SentNotification(StudentCreatedNotificationRecord student)
        {
            _logger.LogInformation($"Driver Notification for {student.StudentFullName}");
            await _bus.Publish(student);
        }
    }
}
