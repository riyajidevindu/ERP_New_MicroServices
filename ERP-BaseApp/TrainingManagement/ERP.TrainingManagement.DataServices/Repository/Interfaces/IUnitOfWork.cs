using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.DataServices.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IInternshipVacancyRepository AddJobRepository { get; }

        IApprovalRequestRepository AddApprovalRequestRepository { get; }

        IFileRepository FileRepository { get; }

        Task<bool> CompleteAsync();
    }
}
