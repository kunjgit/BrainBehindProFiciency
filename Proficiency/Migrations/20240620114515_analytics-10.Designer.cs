﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proficiency.Data;

#nullable disable

namespace Proficiency.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240620114515_analytics-10")]
    partial class analytics10
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Proficiency.Models.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("LectureId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Proficiency.Models.CurrentVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActiveRootAnalyticId")
                        .HasColumnType("int");

                    b.Property<int>("ActiveTTId")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CurrentVersions");
                });

            modelBuilder.Entity("Proficiency.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayName")
                        .HasColumnType("int");

                    b.Property<int?>("TimeTableId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimeTableId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("Proficiency.Models.Lecture", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DayId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("Proficiency.Models.ProfAnalytic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Lectures")
                        .HasColumnType("int");

                    b.Property<string>("Professor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RootAnalyticid")
                        .HasColumnType("int");

                    b.Property<int?>("StudAnalyticId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RootAnalyticid");

                    b.HasIndex("StudAnalyticId");

                    b.ToTable("ProfAnalytics");
                });

            modelBuilder.Entity("Proficiency.Models.RootAnalytic", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("LatestUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalLectures")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("RootAnalytics");
                });

            modelBuilder.Entity("Proficiency.Models.StudAnalytic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("RecentUpate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StuId")
                        .HasColumnType("int");

                    b.Property<int>("TotalLectures")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StudAnalytics");
                });

            modelBuilder.Entity("Proficiency.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Batch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Depart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Proficiency.Models.SubAnalytic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Lectures")
                        .HasColumnType("int");

                    b.Property<int?>("RootAnalyticid")
                        .HasColumnType("int");

                    b.Property<int?>("StudAnalyticId")
                        .HasColumnType("int");

                    b.Property<string>("Sub")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RootAnalyticid");

                    b.HasIndex("StudAnalyticId");

                    b.ToTable("SubAnalytics");
                });

            modelBuilder.Entity("Proficiency.Models.TimeTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Depart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecentUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Sem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeTables");
                });

            modelBuilder.Entity("Proficiency.Models.TimeTableAnalytic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeTableAnalytics");
                });

            modelBuilder.Entity("Proficiency.Models.Day", b =>
                {
                    b.HasOne("Proficiency.Models.TimeTable", null)
                        .WithMany("Days")
                        .HasForeignKey("TimeTableId");
                });

            modelBuilder.Entity("Proficiency.Models.Lecture", b =>
                {
                    b.HasOne("Proficiency.Models.Day", null)
                        .WithMany("Lectures")
                        .HasForeignKey("DayId");
                });

            modelBuilder.Entity("Proficiency.Models.ProfAnalytic", b =>
                {
                    b.HasOne("Proficiency.Models.RootAnalytic", null)
                        .WithMany("Profs")
                        .HasForeignKey("RootAnalyticid");

                    b.HasOne("Proficiency.Models.StudAnalytic", null)
                        .WithMany("Profwise")
                        .HasForeignKey("StudAnalyticId");
                });

            modelBuilder.Entity("Proficiency.Models.SubAnalytic", b =>
                {
                    b.HasOne("Proficiency.Models.RootAnalytic", null)
                        .WithMany("Subs")
                        .HasForeignKey("RootAnalyticid");

                    b.HasOne("Proficiency.Models.StudAnalytic", null)
                        .WithMany("SubWise")
                        .HasForeignKey("StudAnalyticId");
                });

            modelBuilder.Entity("Proficiency.Models.Day", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("Proficiency.Models.RootAnalytic", b =>
                {
                    b.Navigation("Profs");

                    b.Navigation("Subs");
                });

            modelBuilder.Entity("Proficiency.Models.StudAnalytic", b =>
                {
                    b.Navigation("Profwise");

                    b.Navigation("SubWise");
                });

            modelBuilder.Entity("Proficiency.Models.TimeTable", b =>
                {
                    b.Navigation("Days");
                });
#pragma warning restore 612, 618
        }
    }
}
