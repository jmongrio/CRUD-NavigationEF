using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using testNavigation.Models;

namespace testNavigation.Context
{
    public partial class testDBConnectionContext : DbContext
    {
        public testDBConnectionContext()
        {
        }

        public testDBConnectionContext(DbContextOptions<testDBConnectionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Active> Actives { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Active>(entity =>
            {
                entity.ToTable("active");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasMaxLength(40)
                    .HasColumnName("status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdActive).HasColumnName("idActive");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdActiveNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdActive)
                    .HasConstraintName("FK_User_active");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
