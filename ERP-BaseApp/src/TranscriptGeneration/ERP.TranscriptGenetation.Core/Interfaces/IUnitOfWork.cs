using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TranscriptGenetation.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public IResultRepository Results { get; }
        Task<bool> CompleteAsync();
    }
}
