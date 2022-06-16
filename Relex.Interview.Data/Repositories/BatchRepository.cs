using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Data.Repositories
{
    public class BatchRepository : Repository<Batch>
    {
        public BatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
