﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll
{
    public class GetStudentResponse
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public bool present { get; set; }
        public Guid? LabGroupId { get; set; }
    }
}
