using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabEquipmentManagement.Core.Entities
{
    public class LabEquipment :BaseEntity
    {
        public string EquipmentRegisterId { get; set; }
        public string EquipmentName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public string SelectCategory { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public DateTime PurchasedDate { get; set; }




    }
}
