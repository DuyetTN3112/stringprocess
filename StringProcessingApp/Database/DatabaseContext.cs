using StringProcessingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace StringProcessingApp.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(
                        "Server=duyetlaai\\MSSQLSERVER1;Database=StringProcessingDB;Trusted_Connection=True;Encrypt=False;",
                        sqlOptions => sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null))
                            .EnableSensitiveDataLogging() // For debugging
                            .EnableDetailedErrors(); // For debugging
            }
        }

        public bool TestConnection()
        {
            try
            {
                Database.OpenConnection();
                Database.CloseConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.user)
                .WithMany(u => u.messages)
                .HasForeignKey(m => m.user_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.codename)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.phone_number)
                .HasColumnType("nvarchar(15)");

            modelBuilder.Entity<User>()
                .HasMany(u => u.messages)
                .WithOne(m => m.user)
                .HasForeignKey(m => m.user_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Thêm mapping cho tất cả các property để đảm bảo đúng tên cột
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");
                entity.Property(e => e.last_name).HasColumnName("last_name");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.phone_number).HasColumnName("phone_number");
                entity.Property(e => e.codename).HasColumnName("codename");
                entity.Property(e => e.password).HasColumnName("password");
                entity.Property(e => e.role).HasColumnName("role");
                entity.Property(e => e.created_at).HasColumnName("created_at");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.user_id).HasColumnName("user_id");
                entity.Property(e => e.message_text).HasColumnName("message_text");
                entity.Property(e => e.encoded_text).HasColumnName("encoded_text");
                entity.Property(e => e.created_at).HasColumnName("created_at");
            });
        }
    }
}