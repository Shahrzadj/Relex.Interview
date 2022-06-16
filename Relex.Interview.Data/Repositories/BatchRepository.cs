using Relex.Interview.Entities;

namespace Relex.Interview.Data.Repositories
{
    public class BatchRepository : Repository<Batch>
    {
        public BatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
