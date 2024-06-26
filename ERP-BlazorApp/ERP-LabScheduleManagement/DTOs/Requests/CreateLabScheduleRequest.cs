using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LabScheduleManagement.DTOs.Requests
{
    internal class CreateLabScheduleRequest
    {
        public string Batch { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Laboratory { get; set; } = string.Empty;
        public string LabGroups { get; set; } = string.Empty;
        public string LabSpace { get; set; } = string.Empty;
        public string Instructer { get; set; } = string.Empty;
        public DateTime SelectedDate { get; set; }
        public DateTime SelectedTime { get; set; }
        public string LabEquipment { get; set; } = string.Empty;


    }
}
