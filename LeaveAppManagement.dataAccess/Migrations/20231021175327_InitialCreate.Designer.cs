﻿// <auto-generated />
using System;
using LeaveAppManagement.dataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeaveAppManagement.dataAccess.Migrations
{
    [DbContext(typeof(LeaveAppManagementDbContext))]
    [Migration("20231021175327_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.LeaveBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("TotaLeaveAvailable")
                        .HasColumnType("int");

                    b.Property<int>("TotalCurrentLeave")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("TLeaveBalance");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.LeaveCalendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("TLeaveCalendars");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRequest")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Justification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeaveCalendarId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveCalendarId");

                    b.HasIndex("ManagerId");

                    b.ToTable("TLeaveRequest");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("TManager", (string)null);
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TUsers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Admin", b =>
                {
                    b.HasBaseType("LeaveAppManagement.dataAccess.Models.Users");

                    b.ToTable("TAdmin", (string)null);
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Employee", b =>
                {
                    b.HasBaseType("LeaveAppManagement.dataAccess.Models.Users");

                    b.Property<int>("LeaveBalanceId")
                        .HasColumnType("int");

                    b.ToTable("TEmployee", (string)null);
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.LeaveBalance", b =>
                {
                    b.HasOne("LeaveAppManagement.dataAccess.Models.Employee", "Employee")
                        .WithOne("LeaveBalance")
                        .HasForeignKey("LeaveAppManagement.dataAccess.Models.LeaveBalance", "EmployeeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.LeaveRequest", b =>
                {
                    b.HasOne("LeaveAppManagement.dataAccess.Models.Employee", "Employee")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("LeaveAppManagement.dataAccess.Models.LeaveCalendar", "LeaveCalendar")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("LeaveCalendarId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("LeaveAppManagement.dataAccess.Models.Manager", "Manager")
                        .WithMany("LeaveRequestPending")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("LeaveCalendar");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Admin", b =>
                {
                    b.HasOne("LeaveAppManagement.dataAccess.Models.Users", null)
                        .WithOne()
                        .HasForeignKey("LeaveAppManagement.dataAccess.Models.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Employee", b =>
                {
                    b.HasOne("LeaveAppManagement.dataAccess.Models.Users", null)
                        .WithOne()
                        .HasForeignKey("LeaveAppManagement.dataAccess.Models.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.LeaveCalendar", b =>
                {
                    b.Navigation("LeaveRequests");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Manager", b =>
                {
                    b.Navigation("LeaveRequestPending");
                });

            modelBuilder.Entity("LeaveAppManagement.dataAccess.Models.Employee", b =>
                {
                    b.Navigation("LeaveBalance");

                    b.Navigation("LeaveRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
