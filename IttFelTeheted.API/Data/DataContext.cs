using IttFelTeheted.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IttFelTeheted.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Photo>()
        //         .HasKey(t => new { t.UserID, t.PostId});
            
        //     modelBuilder.Entity<Photo>()
        //         .HasOne(ph => ph.User)
        //         .WithMany(u => u.Photos)
        //         .HasForeignKey(ph => ph.UserID);

        //     modelBuilder.Entity<Photo>()
        //         .HasOne(ph => ph.Post)
        //         .WithMany(pt => pt.Photos)
        //         .HasForeignKey(ph => ph.PostId);
        // }

        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<PostedPhoto> PostedPhotos { get; set; }
    }
}