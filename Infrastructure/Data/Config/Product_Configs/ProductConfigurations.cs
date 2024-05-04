using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.Infrastructure.Data.Config.Product_Configs
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.PictureUrl).IsRequired();
            builder.Property(P => P.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand).WithMany().HasForeignKey(P => P.BrandId);
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(p => p.CategoryId);
        }
    }
}
