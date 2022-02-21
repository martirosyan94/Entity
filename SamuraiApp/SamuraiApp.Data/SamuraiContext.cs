using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public class SamuraiContext :  DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SamuraiAppData")
                .LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(b => b.Battles)
                .WithMany(s => s.Samurais)
                .UsingEntity<SamuraiBattle>
                (sb => sb.HasOne<Battle>().WithMany(),
                sb => sb.HasOne<Samurai>().WithMany())
                .Property(sb => sb.JoinedDate)
                .HasDefaultValueSql("getDate()");
        }
        // get-help entityframework
        // add-migration init
    }
}
