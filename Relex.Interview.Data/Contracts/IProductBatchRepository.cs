using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Data.Contracts
{
    public interface IProductBatchRepository : IRepository<ProductBatch>
    {
        IEnumerable<Batch> GetBatchesByProductId(int productId);
    }
}
