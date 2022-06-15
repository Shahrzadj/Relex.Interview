using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Entities
{
    public class BatchSize :BaseEntity
    {
        public string Code { get; set; }
        public int Size { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class BatchSizeConfiguration : IEntityTypeConfiguration<BatchSize>
    {
        public void Configure(EntityTypeBuilder<BatchSize> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.Size).IsRequired();
        }
    }
}
