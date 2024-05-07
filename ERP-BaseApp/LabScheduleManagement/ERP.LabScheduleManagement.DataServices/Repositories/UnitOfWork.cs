using ERP.LabScheduleManagement.DataServices.Data;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.DataServices.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AppDbContext _context;

        public ILabCoordinatorRepository Coordinators { get; }
        public ILabEquipmentRepository Equipments { get; }
        public ILabGroupRepository Groups { get; }
        public ILabInstructorRepository Instructors { get; }
        public ILabRepository Labs { get; }
        public ILabSpaceRepository Spaces { get; }
        public IModuleRepository Modules { get; }
        public IScheduledLabRepository ScheduledLabs { get; }
        public IStudentRepository Students { get; }
        public ITimeSlotRepository TimeSlots { get; }

        public UnitOfWork(AppDbContext context,ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Coordinators = new LabCoordinatorRepository(_context, logger);
            Equipments = new LabEquipmentRepository(_context, logger);
            Groups = new LabGroupRepository(_context, logger);
            Instructors = new LabInstructorRepository(_context, logger);
            Labs = new LabRepository(_context, logger);
            Spaces = new LabSpaceRepository(_context, logger);
            Modules = new ModuleRepository(_context, logger);
            ScheduledLabs = new ScheduledLabRepository(_context, logger);
            Students = new StudentRepository(_context, logger);
            TimeSlots = new TimeSlotRepository(_context, logger);
            
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
