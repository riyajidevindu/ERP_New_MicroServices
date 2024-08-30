using ERP.Messaging.Core.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Messaging.Core.Service.Intefeaces
{
    public interface IStudentCreatedNotificationPublisherService
    {
        Task SentNotification(StudentCreatedNotificationRecord student);
    }
}
