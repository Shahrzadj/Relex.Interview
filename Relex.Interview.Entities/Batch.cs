using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace Relex.Interview.Entities
{
    public class Batch : BaseEntity
    {
        public string Code { get; set; }
        public int Size { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductBatch> BatchProducts { get; set; }
    }
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.Size).IsRequired();
        }
    }
}
