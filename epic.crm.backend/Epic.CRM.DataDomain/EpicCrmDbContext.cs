

using Epic.CRM.DataDomain.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain
{
    public partial class EpicCrmDbContext : IdentityDbContext<IdentityUser>
    {
        public EpicCrmDbContext(DbContextOptions<EpicCrmDbContext> options) :
            base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Address> Address { get; set; }

        public virtual DbSet<AppUser> AppUser { get; set; }

        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<Work> Work { get; set; }

        public virtual DbSet<WorkStatus> WorkStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Address1)
                    .HasMaxLength(250)
                    .HasColumnName("Address");
                entity.Property(e => e.City).HasMaxLength(250);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.AspNetUserId)
                    .IsRequired()
                    .HasMaxLength(450);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
                entity.Property(e => e.Profession)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Navigation(u => u.Customer).AutoInclude();
                entity.Navigation(u => u.Work).AutoInclude();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Address).WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Customer_Address");

                entity.HasOne(d => d.AppUser).WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_AppUser");
            });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(250);
                entity.Property(e => e.TimeStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Address).WithMany(p => p.Work)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Work_Address");

                entity.HasOne(d => d.AppUser).WithMany(p => p.Work)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Work_AppUser");

                entity.HasOne(d => d.Customer).WithMany(p => p.Work)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Work_Customer");

                entity.HasOne(d => d.WorkStatus).WithMany(p => p.Work)
                    .HasForeignKey(d => d.WorkStatusId)
                    .HasConstraintName("FK_Work_WorkStatus");
            });

            modelBuilder.Entity<WorkStatus>(entity =>
            {
                entity.HasKey(e => e.WorkStatusId).HasName("PK_WorkStatus");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
