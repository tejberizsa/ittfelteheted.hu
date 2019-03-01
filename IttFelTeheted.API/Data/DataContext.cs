using IttFelTeheted.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IttFelTeheted.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<PostedPhoto> PostedPhotos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<PostFollow> PostFollows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasKey(k => new {k.VoterId, k.VotedId});

            modelBuilder.Entity<Vote>()
                .HasOne(a => a.Voted)
                .WithMany(u => u.Voter)
                .HasForeignKey(a => a.VotedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(u => u.Voter)
                .WithMany(a => a.Voted)
                .HasForeignKey(u => u.VoterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasKey(uf => new {uf.FollowerId, uf.FollowedId});

            modelBuilder.Entity<UserFollow>()
                .HasOne(u => u.Followed)
                .WithMany(u => u.Follower)
                .HasForeignKey(uf => uf.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(u => u.Follower)
                .WithMany(u => u.Followed)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostFollow>()
                .HasKey(pf => new {pf.FollowerId, pf.FollowedId});

            modelBuilder.Entity<PostFollow>()
                .HasOne(p => p.Followed)
                .WithMany(u => u.PostFollower)
                .HasForeignKey(pf => pf.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostFollow>()
                .HasOne(u => u.Follower)
                .WithMany(p => p.PostFollowed)
                .HasForeignKey(pf => pf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}