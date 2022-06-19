using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Api.Test.MockData
{
    public class ProductBatchMockData
    {
        public static IEnumerable<Batch> GetBatchesByProductId()
        {
            return new List<Batch>
            {
                new Batch {
                Id = 1,
                Code = "B1",
                Size =10
                },
                new Batch {
                Id = 2,
                Code = "B2",
                Size =20
                },
                new Batch {
                Id = 3,
                Code = "B3",
                Size =30
                }
            };
        }
    }
}
