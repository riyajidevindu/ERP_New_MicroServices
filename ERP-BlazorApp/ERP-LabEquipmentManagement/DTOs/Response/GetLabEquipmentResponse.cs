using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_LabEquipmentManagement.DTOs.Response
{
    public class GetLabEquipmentResponse
    {
        public Guid Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string EquipmentRegisterId { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public string SelectCategory { get; set; } = string.Empty;
        public double Price { get; set; } = double.MaxValue;
        public string? Description { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}
