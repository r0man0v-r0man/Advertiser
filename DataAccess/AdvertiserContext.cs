using DataAccess.Entities;
using DataAccess.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class AdvertiserContext : DbContext
    {
        public virtual DbSet<Flat> Flats { get; set; }

        public AdvertiserContext(DbContextOptions<AdvertiserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FlatConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
