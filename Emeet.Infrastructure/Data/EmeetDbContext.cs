using Emeet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeet.Infrastructure.Data
{
    public class EmeetDbContext : DbContext
    {
        public EmeetDbContext() { }
        public EmeetDbContext(DbContextOptions<EmeetDbContext> dbContextOptions) : base(dbContextOptions) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

           // optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnectionString"));
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OTP> OTPs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<ExpertCategory> ExpertCategories { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<StaticFile> StaticFiles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OTP>(entity =>
            {
                entity.ToTable("otp");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").IsRequired();

                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(64).IsRequired(false);

                entity.Property(e => e.OtpKey).HasColumnName("otp_key").HasMaxLength(100).IsRequired(false);

                entity.Property(e => e.CreatedTime).HasColumnName("created_time").IsRequired(false);

                entity.Property(e => e.ExpireTime).HasColumnName("expire_time").IsRequired(false);

                entity.Property(e => e.EndTime).HasColumnName("end_time").IsRequired(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Username).HasMaxLength(50).HasColumnName("username");
                entity.Property(e => e.Password).HasMaxLength(50).HasColumnName("password");
                entity.Property(e => e.FullName).HasMaxLength(50).HasColumnName("full_name");
                entity.Property(e => e.Role).HasMaxLength(20).HasColumnName("role");
                entity.Property(e => e.Avatar).HasColumnName("avatar");
                entity.Property(e => e.Bio).HasColumnName("bio").IsRequired(false);
                entity.Property(e => e.DateCreate).HasColumnName("date_create");
                entity.Property(e => e.DateUpdated).HasColumnName("date_updated").IsRequired(false);
                entity.Property(e => e.RefreshToken).HasMaxLength(100).HasColumnName("refresh_token").IsRequired(false);
                entity.Property(e => e.AccessToken).HasMaxLength(350).HasColumnName("access_token").IsRequired(false);
                entity.Property(e => e.RefreshTokenExpiry).HasColumnName("refresh_token_expiry").IsRequired(false);
                entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
                entity.Property(e => e.Status).HasMaxLength(20).HasColumnName("status");
                entity.Property(e => e.IsExpert).HasColumnName("is_expert");
                entity.Property(e => e.Gender).HasMaxLength(25).HasColumnName("gender");
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("description");
                entity.Property(e => e.DateCreated).HasColumnName("date_created");
                entity.Property(e => e.DateUpdated).HasColumnName("date_updated");
            });

            modelBuilder.Entity<Expert>(entity =>
            {
                entity.ToTable("expert");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Experience).HasMaxLength(255).HasColumnName("experience");
                entity.Property(e => e.PricePerMinute).HasColumnName("price_per_minute");
                entity.Property(e => e.TotalPreview).HasColumnName("total_preview");
                entity.Property(e => e.Rate).HasColumnName("rate");
                entity.Property(e => e.TotalRate).HasColumnName("total_rate");
                entity.Property(e => e.Status).HasMaxLength(25).HasColumnName("status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Experts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_expert");
            });

            modelBuilder.Entity<ExpertCategory>(entity =>
            {
                entity.ToTable("expert_category");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ExpertId).HasColumnName("expert_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.ExpertCategories)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expert_category_expert");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ExpertCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_expert_category_category");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.ExpertId).HasColumnName("expert_id");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
                entity.Property(e => e.EndTime).HasColumnName("end_time");
                entity.Property(e => e.LinkMeet).HasColumnName("link_meet");
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)").HasColumnName("amount");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("description");
                entity.Property(e => e.Status).HasMaxLength(25).HasColumnName("status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appointment_user");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appointment_expert");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Comment).HasColumnName("comment");
                entity.Property(e => e.Rate).HasColumnName("rate");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_appointment");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_expert");
            });
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)").HasColumnName("amount");
                entity.Property(e => e.PaymentMethod).HasMaxLength(25).HasColumnName("payment_method");
                entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
                entity.Property(e => e.DatePaid).HasColumnName("date_paid");
                entity.Property(e => e.DateUpdated).HasColumnName("date_updated");
                entity.Property(e => e.Status).HasMaxLength(25).HasColumnName("status");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_payment_appointment");
            });

            modelBuilder.Entity<StaticFile>(entity =>
            {
                entity.ToTable("static_file");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Link).HasMaxLength(255).HasColumnName("link");
                entity.Property(e => e.Type).HasMaxLength(25).HasColumnName("type");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("description");
                entity.Property(e => e.ExpertId).HasColumnName("expert_id");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.StaticFiles)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_static_file_expert");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ExpertId).HasColumnName("expert_id");
                entity.Property(e => e.DayOfMonth).HasMaxLength(25).HasColumnName("day_of_month");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
                entity.Property(e => e.EndTime).HasColumnName("end_time");
                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ExpertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_schedule_expert");
            });
        }
    }
}
