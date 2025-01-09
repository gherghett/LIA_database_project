﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LIA.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Models.Entities.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StudyProgramId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StudyProgramId");

                    b.ToTable("Classes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SUVNET21",
                            StudyProgramId = 1,
                            Year = 2021
                        },
                        new
                        {
                            Id = 2,
                            Name = "FRONTEND22",
                            StudyProgramId = 2,
                            Year = 2022
                        });
                });

            modelBuilder.Entity("Models.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_DATE");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LocationId = 1,
                            Name = "NetOnNet",
                            Url = "https://netonnet.com"
                        },
                        new
                        {
                            Id = 2,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LocationId = 2,
                            Name = "BitAddict",
                            Url = "https://bitaddict.se"
                        },
                        new
                        {
                            Id = 3,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LocationId = 3,
                            Name = "Advacy/Imerge",
                            Url = "https://www.advacy.se"
                        });
                });

            modelBuilder.Entity("Models.Entities.ContactDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ContactPersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ContactPersonId");

                    b.ToTable("ContactDetails");
                });

            modelBuilder.Entity("Models.Entities.ContactPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Ranking")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ContactPersons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            Name = "Daniel Theliander",
                            Position = "VD",
                            Ranking = 1
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 2,
                            Name = "Helena Bragée",
                            Position = "CEO",
                            Ranking = 1
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 3,
                            Name = "Roland Svensson",
                            Position = "VD",
                            Ranking = 1
                        });
                });

            modelBuilder.Entity("Models.Entities.Employment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EmploymentDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StudentId");

                    b.ToTable("Employments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            EmploymentDate = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 2,
                            EmploymentDate = new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StudentId = 2
                        });
                });

            modelBuilder.Entity("Models.Entities.InterestApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("InterestApps");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            Year = "2024"
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 2,
                            Year = "2023"
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 3,
                            Year = "2024"
                        });
                });

            modelBuilder.Entity("Models.Entities.LiaPitch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasOccurred")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("LiaPitches");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyId = 1,
                            HasOccurred = false,
                            Year = "2024"
                        },
                        new
                        {
                            Id = 2,
                            CompanyId = 2,
                            HasOccurred = true,
                            Year = "2023"
                        },
                        new
                        {
                            Id = 3,
                            CompanyId = 3,
                            HasOccurred = false,
                            Year = "2024"
                        });
                });

            modelBuilder.Entity("Models.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Borås"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Göteborg"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ulricehamn"
                        });
                });

            modelBuilder.Entity("Models.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClassId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StudyProgramId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("StudyProgramId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClassId = 1,
                            Name = "Alice Andersson",
                            StudyProgramId = 1
                        },
                        new
                        {
                            Id = 2,
                            ClassId = 2,
                            Name = "Bob Bengtsson",
                            StudyProgramId = 2
                        });
                });

            modelBuilder.Entity("Models.Entities.StudyProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StudyPrograms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SUVNET"
                        },
                        new
                        {
                            Id = 2,
                            Name = "FRONTEND"
                        });
                });

            modelBuilder.Entity("Models.Entities.Class", b =>
                {
                    b.HasOne("Models.Entities.StudyProgram", "StudyProgram")
                        .WithMany("Classes")
                        .HasForeignKey("StudyProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudyProgram");
                });

            modelBuilder.Entity("Models.Entities.Company", b =>
                {
                    b.HasOne("Models.Entities.Location", "Location")
                        .WithMany("Companies")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Models.Entities.ContactDetail", b =>
                {
                    b.HasOne("Models.Entities.ContactPerson", "ContactPerson")
                        .WithMany("ContactDetails")
                        .HasForeignKey("ContactPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactPerson");
                });

            modelBuilder.Entity("Models.Entities.ContactPerson", b =>
                {
                    b.HasOne("Models.Entities.Company", "Company")
                        .WithMany("ContactPersons")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Models.Entities.Employment", b =>
                {
                    b.HasOne("Models.Entities.Company", "Company")
                        .WithMany("Employments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.Student", "Student")
                        .WithMany("Employments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Models.Entities.InterestApp", b =>
                {
                    b.HasOne("Models.Entities.Company", "Company")
                        .WithMany("InterestApplications")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Models.Entities.LiaPitch", b =>
                {
                    b.HasOne("Models.Entities.Company", "Company")
                        .WithMany("LIAPitches")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Models.Entities.Student", b =>
                {
                    b.HasOne("Models.Entities.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.StudyProgram", "StudyProgram")
                        .WithMany("Students")
                        .HasForeignKey("StudyProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("StudyProgram");
                });

            modelBuilder.Entity("Models.Entities.Class", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Models.Entities.Company", b =>
                {
                    b.Navigation("ContactPersons");

                    b.Navigation("Employments");

                    b.Navigation("InterestApplications");

                    b.Navigation("LIAPitches");
                });

            modelBuilder.Entity("Models.Entities.ContactPerson", b =>
                {
                    b.Navigation("ContactDetails");
                });

            modelBuilder.Entity("Models.Entities.Location", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("Models.Entities.Student", b =>
                {
                    b.Navigation("Employments");
                });

            modelBuilder.Entity("Models.Entities.StudyProgram", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
