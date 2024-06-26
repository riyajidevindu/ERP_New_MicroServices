﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests
{
    public class CreateScheduledLabRequest
    {
        public bool Resheduled { get; set; }
        public bool Completed { get; set; }

        public Guid? LabInstructorId { get; set; }
        public Guid? LabCoordinatorId { get; set; }
        public Guid? LabId { get; set; }
        public Guid? TimeSlotId { get; set; }
        public Guid? LabSpaceId { get; set; }
    }
}
