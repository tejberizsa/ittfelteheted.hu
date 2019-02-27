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
        }
    }
}