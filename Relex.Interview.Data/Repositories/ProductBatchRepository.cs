using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Data.Repositories
{
    public class ProductBatchRepository : Repository<ProductBatch>, IProductBatchRepository
    {
        public ProductBatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Batch> GetBatchesByProductId(int productId)
        {
            return Entities.Where(i => i.ProductId == productId).Select(i => i.Batch);
        }
    }
}
