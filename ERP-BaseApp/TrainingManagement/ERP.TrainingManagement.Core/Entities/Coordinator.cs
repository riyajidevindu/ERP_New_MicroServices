﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class Coordinator : ApplicationUser
    {
        public ICollection<InternshipVacancy> InternshipVacancies { get; set; }
        public ICollection<ApprovalRequest> ApprovedRequests { get; set; }
    }
}
