﻿// <auto-generated />
using System;
using LineCon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LineCon.Migrations
{
    [DbContext(typeof(LineConContext))]
    partial class LineConContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LineCon.Models.Attendee", b =>
                {
                    b.Property<Guid>("AttendeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BadgeName")
                        .IsRequired();

                    b.Property<string>("ConfirmationNumber")
                        .IsRequired();

                    b.Property<Guid>("ConventionId");

                    b.HasKey("AttendeeId");

                    b.HasIndex("ConventionId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("LineCon.Models.AttendeeTicket", b =>
                {
                    b.Property<Guid>("AttendeeTicketId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AttendeeId");

                    b.Property<bool>("Completed");

                    b.Property<Guid>("ConventionId");

                    b.Property<Guid>("TicketWindowId");

                    b.HasKey("AttendeeTicketId");

                    b.HasIndex("AttendeeId");

                    b.HasIndex("ConventionId");

                    b.HasIndex("TicketWindowId");

                    b.ToTable("AttendeeTickets");
                });

            modelBuilder.Entity("LineCon.Models.ConConfig", b =>
                {
                    b.Property<Guid>("ConConfigId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConventionId");

                    b.Property<bool>("RequireConfirmationNumber");

                    b.Property<int>("TicketWindowCapacity");

                    b.Property<TimeSpan>("TicketWindowInterval");

                    b.HasKey("ConConfigId");

                    b.HasIndex("ConventionId");

                    b.ToTable("ConConfigs");
                });

            modelBuilder.Entity("LineCon.Models.ConfirmationNumber", b =>
                {
                    b.Property<Guid>("ConConfigId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConventionId");

                    b.Property<string>("Number");

                    b.HasKey("ConConfigId");

                    b.HasIndex("ConventionId");

                    b.ToTable("ConfirmationNumbers");
                });

            modelBuilder.Entity("LineCon.Models.Convention", b =>
                {
                    b.Property<Guid>("ConventionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConConfigId");

                    b.Property<string>("UrlIdentifier");

                    b.HasKey("ConventionId");

                    b.HasIndex("ConConfigId");

                    b.ToTable("Conventions");
                });

            modelBuilder.Entity("LineCon.Models.RegistrationHours", b =>
                {
                    b.Property<Guid>("RegistrationHoursId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConConfigId");

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("RegistrationHoursId");

                    b.HasIndex("ConConfigId");

                    b.ToTable("RegistrationHours");
                });

            modelBuilder.Entity("LineCon.Models.TicketWindow", b =>
                {
                    b.Property<Guid>("TicketWindowId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AttendeeCapacity");

                    b.Property<Guid>("ConventionId");

                    b.Property<TimeSpan>("Length");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("TicketWindowId");

                    b.HasIndex("ConventionId");

                    b.ToTable("TicketWindows");
                });

            modelBuilder.Entity("LineCon.Models.Attendee", b =>
                {
                    b.HasOne("LineCon.Models.Convention", "Convention")
                        .WithMany("Attendees")
                        .HasForeignKey("ConventionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LineCon.Models.AttendeeTicket", b =>
                {
                    b.HasOne("LineCon.Models.Attendee", "Attendee")
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LineCon.Models.Convention", "Convention")
                        .WithMany("AttendeeTickets")
                        .HasForeignKey("ConventionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LineCon.Models.TicketWindow", "TicketWindow")
                        .WithMany("AttendeeTickets")
                        .HasForeignKey("TicketWindowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LineCon.Models.ConConfig", b =>
                {
                    b.HasOne("LineCon.Models.Convention", "Convention")
                        .WithMany()
                        .HasForeignKey("ConventionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LineCon.Models.ConfirmationNumber", b =>
                {
                    b.HasOne("LineCon.Models.Convention", "Convention")
                        .WithMany("ConfirmationNumbers")
                        .HasForeignKey("ConventionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LineCon.Models.Convention", b =>
                {
                    b.HasOne("LineCon.Models.ConConfig", "ConConfig")
                        .WithMany()
                        .HasForeignKey("ConConfigId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LineCon.Models.RegistrationHours", b =>
                {
                    b.HasOne("LineCon.Models.ConConfig", "ConConfig")
                        .WithMany("RegistrationHours")
                        .HasForeignKey("ConConfigId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LineCon.Models.TicketWindow", b =>
                {
                    b.HasOne("LineCon.Models.Convention", "Convention")
                        .WithMany("TicketWindows")
                        .HasForeignKey("ConventionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
