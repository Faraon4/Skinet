using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    // This file is used as DataBase Code First
    // Here we are Writing the configuration that we want to apply to our properties
    // We can do this in the context , but that file can become really big
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           builder.Property(p => p.Id).IsRequired();
           builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
           builder.Property(p => p.Description).IsRequired();
           builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
           builder.Property(p => p.PictureUrl).IsRequired();

           
           // One products has a single brand that is related to , 
           // and add .WithMany -> because each brand can be associated with many products
           builder.HasOne(b => b.ProductBrand).WithMany()
                  .HasForeignKey(p => p.ProductBrandId);
            
            builder.HasOne( t => t.ProductType).WithMany()
                    .HasForeignKey(p => p.ProductTypeId);
        }
    }
}