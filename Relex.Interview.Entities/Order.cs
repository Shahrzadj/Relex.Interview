using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Entities
{
    public class Order: BaseEntity
    {
        public int NumberOfBatches { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BatchSizeId { get; set; }
        public BatchSize BatchSize { get; set; }
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NumberOfBatches).IsRequired();
            builder.HasOne(p => p.Product).WithMany(p => p.Orders).HasForeignKey(f => f.ProductId);
            builder.HasOne(p => p.BatchSize).WithMany(p => p.Orders).HasForeignKey(f => f.BatchSizeId);
        }
    }
}
