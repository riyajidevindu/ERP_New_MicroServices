using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll
{
    public class GetLabGroupResponse
    {
        public Guid LabGroupId { get; set; }
        public string GroupNumber { get; set; }
        public int Batch { get; set; }
        public string Specilization { get; set; }
        public int NoOfStudents { get; set; }
    }
}
