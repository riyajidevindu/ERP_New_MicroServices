using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetById
{
    public class GetLabInstructorByIdResponse
    {
        public Guid LabInstructorId { get; set; }
        public string InstructorName { get; set; }
        public string EmailAddress { get; set; }
    }
}
