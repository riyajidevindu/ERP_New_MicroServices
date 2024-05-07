using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests
{
    public class CreateLabGroupRequest
    {
        public string GroupNumber { get; set; }
        public int Batch { get; set; }
        public string Specilization { get; set; }
        public int NoOfStudents { get; set; }
    }
}
