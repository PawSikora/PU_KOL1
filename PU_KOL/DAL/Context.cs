using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) 
        { }

        public DbSet<Student> Studenci { get; set; }
        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<Historia> Historie { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(e => e.Grupa)
                    .WithMany(g => g.Studenci)
                    .HasForeignKey(e => e.IDGrupy)
                    .OnDelete(DeleteBehavior.SetNull);
            });


            modelBuilder.Entity<Grupa>(entity =>
            {
                entity.ToTable("Grupa");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<Historia>(entity =>
            {
                entity.ToTable("Historia");
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypAkcji)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Data)
                    .IsRequired();
            });
        }
    }
}
