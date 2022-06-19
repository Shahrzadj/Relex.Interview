using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
