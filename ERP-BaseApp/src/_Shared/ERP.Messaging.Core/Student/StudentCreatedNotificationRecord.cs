using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Messaging.Core.Student
{
    public record StudentCreatedNotificationRecord
    (
        Guid StudentId,
        string StudentFullName,
        string RegistrationNumber,
        DateTime DateOfBirth  
     );
}
