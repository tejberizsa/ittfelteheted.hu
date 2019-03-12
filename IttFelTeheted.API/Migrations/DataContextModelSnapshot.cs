﻿// <auto-generated />
using System;
using IttFelTeheted.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IttFelTeheted.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IttFelTeheted.API.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("IttFelTeheted.API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime?>("DateRead");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime>("MessageSent");

                    b.Property<bool>("RecipientDeleted");

                    b.Property<int>("RecipientId");

                    b.Property<bool>("SenderDeleted");

                    b.Property<int>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

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

            modelBuilder.Entity("IttFelTeheted.API.Models.PostedPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<bool>("IsMain");

                    b.Property<int>("PostId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostedPhotos");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.PostFollow", b =>
                {
                    b.Property<int>("FollowerId");

                    b.Property<int>("FollowedId");

                    b.HasKey("FollowerId", "FollowedId");

                    b.HasIndex("FollowedId");

                    b.ToTable("PostFollows");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon");

                    b.Property<string>("TopicName");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("EMail");

                    b.Property<string>("Gender");

                    b.Property<string>("Introduction");

                    b.Property<DateTime>("LastActive");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.UserFollow", b =>
                {
                    b.Property<int>("FollowerId");

                    b.Property<int>("FollowedId");

                    b.HasKey("FollowerId", "FollowedId");

                    b.HasIndex("FollowedId");

                    b.ToTable("UserFollows");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.UserPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Url");

                    b.Property<int>("UserID");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("UserPhotos");
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Vote", b =>
                {
                    b.Property<int>("VoterId");

                    b.Property<int>("VotedId");

                    b.Property<bool>("IsCorrect");

                    b.HasKey("VoterId", "VotedId");

                    b.HasIndex("VotedId");

                    b.ToTable("Votes");
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

            modelBuilder.Entity("IttFelTeheted.API.Models.Message", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.User", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IttFelTeheted.API.Models.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
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

            modelBuilder.Entity("IttFelTeheted.API.Models.PostedPhoto", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.Post", "Post")
                        .WithMany("Photos")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.PostFollow", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.Post", "Followed")
                        .WithMany("PostFollower")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IttFelTeheted.API.Models.User", "Follower")
                        .WithMany("PostFollowed")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.UserFollow", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.User", "Followed")
                        .WithMany("Follower")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IttFelTeheted.API.Models.User", "Follower")
                        .WithMany("Followed")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.UserPhoto", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IttFelTeheted.API.Models.Vote", b =>
                {
                    b.HasOne("IttFelTeheted.API.Models.Answer", "Voted")
                        .WithMany("Voter")
                        .HasForeignKey("VotedId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IttFelTeheted.API.Models.User", "Voter")
                        .WithMany("Voted")
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
