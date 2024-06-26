using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll
{
    public class GetTimeSlotResponse
    {
        public Guid TimeSlotId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public double Duration { get; set; }
        public DateOnly BookedDate { get; set; }
        public DateOnly? RescheduledDate { get; set; }
    }
}
