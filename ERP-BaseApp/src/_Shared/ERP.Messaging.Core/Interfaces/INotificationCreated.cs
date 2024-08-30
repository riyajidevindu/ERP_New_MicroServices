using ERP.Messaging.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Messaging.Core.Interfaces
{
    public interface INotificationCreated
    {
        DateTime NotificationDate { get; }
        string NotificationMessage { get; }
        CreatedNotificationType NotificationType { get; }
    }
}
