﻿using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LabEquipmentManagement.DTOs.Request
{
    public class CreateLabEquipmentRequest
    {
        public string Location { get; set; } = string.Empty;
        public string EquipmentRegisterId { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public string SelectCategory { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? Description { get; set; }
        public DateTime? PurchasedDate { get; set; }

       
    }
}
