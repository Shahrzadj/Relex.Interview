using Relex.Interview.Api.Dtos.Product;
using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Api.Test.MockData
{
    public class ProductMockData
    {
        public static IEnumerable<Product> GetAll()
        {
            return new List<Product> {
                new Product { 
                    Id = 1,
                    Code = "P1",
                    Name = "Milk",
                    Price = 1.60M
                },
                new Product {
                    Id = 2,
                    Code = "P2",
                    Name = "Suger",
                    Price = 0.88M
                },
                new Product {
                    Id = 3,
                    Code = "P3",
                    Name = "Bread",
                    Price = 3.60M
                },
            };
        }
    }
}
