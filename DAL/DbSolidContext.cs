using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using SolidTA.DAL.Entities;
namespace SolidTA.DAL
{
    public partial class DbSolidContext : DbContext
    {
        public DbSolidContext()
            : base("name=DbSolidConfig")
        {
        }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.NumCode)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.CharCode)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Rate)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.Value)
                .HasPrecision(18, 4);
        }
    }
}
