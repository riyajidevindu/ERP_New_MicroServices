﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class CVUpload : BaseEntity
    {
        public string FilePath { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
