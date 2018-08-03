using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Promociones.Domain.Entities;

namespace Promociones.Infrastructure
{
    public class PromocionesDbContext : DbContext
    {
        public PromocionesDbContext()
        {
        }

        public PromocionesDbContext(DbContextOptions<PromocionesDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Promocion>()
                .Property(b => b.PorcentajeDescuento).HasColumnType("float");
        }

        

        public DbSet<Promocion> Promociones { get; set; }

        
    }
}
