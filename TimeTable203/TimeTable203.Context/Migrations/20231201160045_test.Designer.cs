﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTable203.Context;

#nullable disable

namespace TimeTable203.Context.Migrations
{
    [DbContext(typeof(TimeTableContext))]
    [Migration("20231201160045_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Discipline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Discipline_Name")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Disciplines", (string)null);
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("Number", "Series")
                        .IsUnique()
                        .HasDatabaseName("IX_Document_Number_Series")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EmployeeType")
                        .HasColumnType("int");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("IX_Group_Name")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Person_Email")
                        .HasFilter("DeletedAt is null");

                    b.HasIndex("GroupId");

                    b.HasIndex("Phone")
                        .IsUnique()
                        .HasDatabaseName("IX_Person_Phone")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.TimeTableItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("DisciplineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("RoomNumber")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("GroupId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("StartDate", "EndDate")
                        .HasDatabaseName("IX_TimeTableItem_StartDate_EndDate")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("TimeTableItems", (string)null);
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Document", b =>
                {
                    b.HasOne("TimeTable203.Context.Contracts.Models.Person", "Person")
                        .WithMany("Document")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Employee", b =>
                {
                    b.HasOne("TimeTable203.Context.Contracts.Models.Person", "Person")
                        .WithMany("Employee")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Group", b =>
                {
                    b.HasOne("TimeTable203.Context.Contracts.Models.Employee", "Employee")
                        .WithMany("Group")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Person", b =>
                {
                    b.HasOne("TimeTable203.Context.Contracts.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.TimeTableItem", b =>
                {
                    b.HasOne("TimeTable203.Context.Contracts.Models.Discipline", "Discipline")
                        .WithMany("TimeTableItem")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTable203.Context.Contracts.Models.Group", "Group")
                        .WithMany("TimeTableItem")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTable203.Context.Contracts.Models.Employee", "Teacher")
                        .WithMany("TimeTableItem")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Discipline");

                    b.Navigation("Group");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Discipline", b =>
                {
                    b.Navigation("TimeTableItem");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Employee", b =>
                {
                    b.Navigation("Group");

                    b.Navigation("TimeTableItem");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Group", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("TimeTableItem");
                });

            modelBuilder.Entity("TimeTable203.Context.Contracts.Models.Person", b =>
                {
                    b.Navigation("Document");

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}