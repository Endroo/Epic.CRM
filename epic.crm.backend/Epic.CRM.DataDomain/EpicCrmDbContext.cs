

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
        { }

        public virtual DbSet<Cim> Cim { get; set; }

        public virtual DbSet<Felhasznalo> Felhasznalo { get; set; }

        public virtual DbSet<Munka> Munka { get; set; }

        public virtual DbSet<MunkaStatusz> MunkaStatusz { get; set; }

        public virtual DbSet<Ugyfel> Ugyfel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cim>(entity =>
            {
                entity.Property(e => e.CimId).HasColumnName("CimID");
                entity.Property(e => e.Telepules).HasMaxLength(250);
                entity.Property(e => e.UtcaHazszam).HasMaxLength(250);
            });

            modelBuilder.Entity<Felhasznalo>(entity =>
            {
                entity.Property(e => e.FelhasznaloId).HasColumnName("FelhasznaloID");
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.Jelszo).HasMaxLength(250);
                entity.Property(e => e.Nev).HasMaxLength(250);
                entity.Property(e => e.Tevekenyseg).HasMaxLength(250);
            });

            modelBuilder.Entity<Munka>(entity =>
            {
                entity.Property(e => e.MunkaId).HasColumnName("MunkaID");
                entity.Property(e => e.CimId).HasColumnName("CimID");
                entity.Property(e => e.FelhasznaloId).HasColumnName("FelhasznaloID");
                entity.Property(e => e.Idopont).HasColumnType("datetime");
                entity.Property(e => e.MunkaMegnevezes).HasMaxLength(250);
                entity.Property(e => e.StatuszId).HasColumnName("StatuszID");
                entity.Property(e => e.UgyfelId).HasColumnName("UgyfelID");

                entity.HasOne(d => d.Cim).WithMany(p => p.Munka)
                    .HasForeignKey(d => d.CimId)
                    .HasConstraintName("FK_Munka_Cim");

                entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Munka)
                    .HasForeignKey(d => d.FelhasznaloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Munka_Felhasznalo");

                entity.HasOne(d => d.Statusz).WithMany(p => p.Munka)
                    .HasForeignKey(d => d.StatuszId)
                    .HasConstraintName("FK_Munka_MunkaStatusz");

                entity.HasOne(d => d.Ugyfel).WithMany(p => p.Munka)
                    .HasForeignKey(d => d.UgyfelId)
                    .HasConstraintName("FK_Munka_Ugyfel");
            });

            modelBuilder.Entity<MunkaStatusz>(entity =>
            {
                entity.Property(e => e.MunkaStatuszId).HasColumnName("MunkaStatuszID");
                entity.Property(e => e.StatuszMegnevezes)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ugyfel>(entity =>
            {
                entity.Property(e => e.UgyfelId).HasColumnName("UgyfelID");
                entity.Property(e => e.CimId).HasColumnName("CimID");
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.FelhasznaloId).HasColumnName("FelhasznaloID");
                entity.Property(e => e.Nev).HasMaxLength(250);

                entity.HasOne(d => d.Cim).WithMany(p => p.Ugyfel)
                    .HasForeignKey(d => d.CimId)
                    .HasConstraintName("FK_Ugyfel_Cim");

                entity.HasOne(d => d.Felhasznalo).WithMany(p => p.Ugyfel)
                    .HasForeignKey(d => d.FelhasznaloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ugyfel_Felhasznalo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
