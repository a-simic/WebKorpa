using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebKorpa.Models
{
    public partial class ProdavnicaContext : DbContext
    {
        public ProdavnicaContext()
        {
        }

        public ProdavnicaContext(DbContextOptions<ProdavnicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kategorija> Kategorije { get; set; }
        public virtual DbSet<Kupac> Kupci { get; set; }
        public virtual DbSet<Porudzbina> Porudzbine { get; set; }
        public virtual DbSet<Proizvod> Proizvodi { get; set; }
        public virtual DbSet<Stavka> Stavke { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Kupac>(entity =>
            {
                entity.Property(e => e.KupacId).ValueGeneratedNever();

                entity.Property(e => e.Drzava).HasDefaultValueSql("('Srbija')");

                entity.Property(e => e.Grad).HasDefaultValueSql("('Beograd')");
            });

            modelBuilder.Entity<Porudzbina>(entity =>
            {
                entity.Property(e => e.DatumKupovine).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Kupac)
                    .WithMany(p => p.Porudzbine)
                    .HasForeignKey(d => d.KupacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Porudzbin__Kupac__4D94879B");
            });

            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.HasOne(d => d.Kategorija)
                    .WithMany(p => p.Proizvodi)
                    .HasForeignKey(d => d.KategorijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Proizvod__Katego__534D60F1");
            });

            modelBuilder.Entity<Stavka>(entity =>
            {
                entity.HasIndex(e => new { e.PorudzbinaId, e.ProizvodId })
                    .HasName("UQ_Stavka")
                    .IsUnique();

                entity.HasOne(d => d.Porudzbina)
                    .WithMany(p => p.Stavke)
                    .HasForeignKey(d => d.PorudzbinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stavka__Porudzbi__571DF1D5");

                entity.HasOne(d => d.Proizvod)
                    .WithMany(p => p.Stavke)
                    .HasForeignKey(d => d.ProizvodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stavka__Proizvod__5812160E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}