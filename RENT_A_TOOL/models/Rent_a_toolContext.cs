using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RENT_A_TOOL.models
{
    internal class Rent_a_toolContext : DbContext
    {
        public DbSet<Użytkownik> Użytkownicy { get; set; }
        public DbSet<Sprzęt> Sprzęt { get; set; }
        public DbSet<Wypożyczenie> Wypożyczenia { get; set; }
        //public object Wypożyczenie { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-2R2BO2O\\SQLEXPRESS;Database=rent-a-tool;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wypożyczenie>()
                .HasOne(w => w.Klient)
                .WithMany()
                .HasForeignKey(w => w.ID_Klienta)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Wypożyczenie>()
                .HasOne(w => w.Sprzet)
                .WithMany()
                .HasForeignKey(w => w.ID_Sprzet)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
