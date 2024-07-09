using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class InternshipVacancy : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }

        public virtual ICollection<CVUpload> CVUploads { get; set; }

        //public Guid CoordinatorId { get; set; }
        // public Coordinator Coordinator { get; set; }

    }
}
