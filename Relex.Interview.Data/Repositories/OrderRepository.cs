using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
