using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public int status { get; set; } 

    }
}
