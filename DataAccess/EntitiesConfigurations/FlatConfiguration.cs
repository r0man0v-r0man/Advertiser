using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntitiesConfigurations
{
    public class FlatConfiguration : IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder
                .Property(flat => flat.Id);
            builder
                .Property(flat => flat.Rooms)
                .IsRequired()
                .HasMaxLength(10);
            builder
                .Property(flat => flat.IsActive)
                .IsRequired();
            builder
                .Property(flat => flat.Price)
                .IsRequired();
            builder
                .Property(flat => flat.Description)
                .IsRequired()
                .HasMaxLength(255);

        }
    }
}
