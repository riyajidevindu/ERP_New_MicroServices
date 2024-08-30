using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.Core.Contracts;

public record StudentCreatedNotification
(Guid StudentId, string RegistrationNumber);
