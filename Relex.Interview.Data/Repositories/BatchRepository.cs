using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Data.Repositories
{
    public class BatchRepository : Repository<Batch>, IBatchRepository
    {
        public BatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
