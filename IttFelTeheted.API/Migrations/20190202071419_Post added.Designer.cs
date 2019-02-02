﻿// <auto-generated />
using System;
using IttFelTeheted.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IttFelTeheted.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190202071419_Post added")]
    partial class Postadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("IttFelTeheted.API.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerBody");

                    b.Property<DateTime>("DateAdded");

                    b.Property<int>("Like");

                    b.Property<string>("PhotoUrl");

                    b.Property<int>("PostId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("PostBody");

                    b.Property<string>("Title");

                    b.Property<int?>("TopicId");

                    b.Property<int?>("UserId");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TopicName");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("EMail");

                    b.Property<string>("Gender");

                    b.Property<string>("Introduction");

                    b.Property<DateTime>("LastActive");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhotoUrl");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Answer", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.Post", "Post")
                        .WithMany("Answers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IttFelTeheted.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Post", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("IttFelTeheted.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
