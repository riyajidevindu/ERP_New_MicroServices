﻿// <auto-generated />
using System;
using ERP.WorkLoadManagement.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERP.WorkLoadManagement.DataService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240618172531_nullablefileds")]
    partial class nullablefileds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("ERP.WorkLoadManagement.Core.Entities.AssignWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AssignByUserId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRejected")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("StaffId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("WorkId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.HasIndex("WorkId");

                    b.ToTable("AssignWorks");
                });

            modelBuilder.Entity("ERP.WorkLoadManagement.Core.Entities.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("ERP.WorkLoadManagement.Core.Entities.Work", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("ERP.WorkLoadManagement.Core.Entities.AssignWork", b =>
                {
                    b.HasOne("ERP.WorkLoadManagement.Core.Entities.Staff", "Staff")
                        .WithMany("AssignWorks")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ERP.WorkLoadManagement.Core.Entities.Work", "Work")
                        .WithMany("AssignWorks")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Staff");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("ERP.WorkLoadManagement.Core.Entities.Staff", b =>
                {
                    b.Navigation("AssignWorks");
                });

            modelBuilder.Entity("ERP.WorkLoadManagement.Core.Entities.Work", b =>
                {
                    b.Navigation("AssignWorks");
                });
#pragma warning restore 612, 618
        }
    }
}
