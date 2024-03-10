
using Bogus;
using ERP.TranscriptGenetation.Core.Entity;
using ERP.TranscriptGenetation.Core.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TranscriptGeneration.DataService
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var students = Enumerable.Range(1, 1).Select(
                          index => new Faker<Student>()
                         .RuleFor(s => s.FullName, f => f.Name.FullName())
                         .RuleFor(s => s.RegNo, f => $"EG/{f.Random.Int(2020, 2023)}/{f.Random.Int(1000, 9999)}")
                         .RuleFor(s => s.DateOfBirth, f => DateTime.Now)
                         .RuleFor(s => s.Gender, f => "Male")
                         .RuleFor(s => s.Status, f => 1)
                         .Generate()).ToList();


            

            var results = new List<Result>
            {
                 new Result
                    {
                        StudentId = students[0].Id,
                        ModuleName = "Intoduction to Algorithms",
                        ModuleCode = "EE1101",
                        Credits = 1,
                        GPV = 3.7,
                        Grade = "A-",
                        Semester = "semestr 1",
                        Type = "CM",
                        Status = 1
                       
                    },

                    new Result
                    {
                        StudentId = students[0].Id,
                        ModuleName = "Intoduction to Electronics",
                        ModuleCode = "EE1102",
                        Credits = 2,
                        GPV = 2.3,
                        Grade = "C+",
                        Semester = "semestr 1",
                        Type = "CM",
                        Status = 1

                    },

                     new Result
                     {
                         StudentId = students[0].Id,
                         ModuleName = "Electricity",
                         ModuleCode = "EE1302",
                         Credits = 2,
                         GPV = 3.0,
                         Grade = "B",
                         Semester = "semestr 1",
                         Type = "CM",
                        Status = 1

                     }
            };

            for(int i=0;i< 10; i++)
            {
                results.Add(
                     new Result
                     {
                         StudentId = students[0].Id,
                         ModuleName = "Electricity",
                         ModuleCode = "EE1302",
                         Credits = 2,
                         GPV = 4.0,
                         Grade = "A+",
                         Semester = "semestr 1",
                         Type = "CM",
                         Status = 1

                     }
              );

            }


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasData(students);


            modelBuilder.Entity<Result>()
                .HasData(results);

                     
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Result> Results { get; set; }
    }
}
