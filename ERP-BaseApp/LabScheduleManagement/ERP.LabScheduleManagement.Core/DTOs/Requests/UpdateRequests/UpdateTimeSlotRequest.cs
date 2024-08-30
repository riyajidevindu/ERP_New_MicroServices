using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests
{
    public class UpdateTimeSlotRequest
    {
        public Guid TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public DateTime BookedDate { get; set; }
        public DateTime? RescheduledDate { get; set; }
    }
}
