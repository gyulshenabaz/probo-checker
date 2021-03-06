﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using probo_checker.DataAccess.Context;

namespace probo_checker.DataAccess.Migrations
{
    [DbContext(typeof(ProboCheckerDbContext))]
    [Migration("20190919122835_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("probo_checker.DataAccess.Models.ApiKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Key")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ApiKeys");
                });

            modelBuilder.Entity("probo_checker.DataAccess.Models.Parameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("TestCaseId");

                    b.Property<string>("Type");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("TestCaseId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("probo_checker.DataAccess.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Language");

                    b.Property<string>("Message");

                    b.Property<string>("ProblemName");

                    b.Property<double>("Score");

                    b.HasKey("Id");

                    b.ToTable("Submission");
                });

            modelBuilder.Entity("probo_checker.DataAccess.Models.TestCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActualOutput");

                    b.Property<string>("ExpectedOutput");

                    b.Property<int>("SubmissionId");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId");

                    b.ToTable("TestCases");
                });

            modelBuilder.Entity("probo_checker.DataAccess.Models.Parameter", b =>
                {
                    b.HasOne("probo_checker.DataAccess.Models.TestCase", "TestCase")
                        .WithMany("Parameters")
                        .HasForeignKey("TestCaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("probo_checker.DataAccess.Models.TestCase", b =>
                {
                    b.HasOne("probo_checker.DataAccess.Models.Submission", "Submission")
                        .WithMany("Tests")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
