using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Entities
{
    public class ProductBatch: IEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BatchId { get; set; }
        public Batch Batch { get; set; }
    }

    public class ProductBatchConfiguration : IEntityTypeConfiguration<ProductBatch>
    {
        public void Configure(EntityTypeBuilder<ProductBatch> builder)
        {
            builder.HasKey(t => new { t.ProductId, t.BatchId });

            builder
            .HasOne(pt => pt.Product)
            .WithMany(p => p.BatchProducts)
            .HasForeignKey(pt => pt.ProductId);

            builder
                .HasOne(pt => pt.Batch)
                .WithMany(t => t.BatchProducts)
                .HasForeignKey(pt => pt.BatchId);

            builder.Navigation(p => p.Product).AutoInclude();
            builder.Navigation(p => p.Batch).AutoInclude();
        }
    }
}
