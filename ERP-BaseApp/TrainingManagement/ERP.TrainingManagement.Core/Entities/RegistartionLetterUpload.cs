using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class RegistartionLetterUpload : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual Student Student { get; set; }
    }
}
