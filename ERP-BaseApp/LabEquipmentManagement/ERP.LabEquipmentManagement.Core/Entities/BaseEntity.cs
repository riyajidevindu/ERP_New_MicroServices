using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabEquipmentManagement.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        public DateTime AddDate { get; set; } =DateTime.UtcNow;

        public int Status { get; set; }

    }
}
