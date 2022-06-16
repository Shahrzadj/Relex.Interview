using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relex.Interview.Api.Dtos.Product;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{id}")]   
        public async Task<ProductDto> Get(int id)
        {
            var product= await _productRepository.GetByIdAsync(1, CancellationToken.None);
            var output = new ProductDto()
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Price = product.Price
            };
            return output;
        }
    }
}
