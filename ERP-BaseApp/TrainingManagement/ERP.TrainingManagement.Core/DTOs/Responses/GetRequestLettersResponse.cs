﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class GetRequestLettersResponse 
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
    }
}
