using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<Product> Get(int id)
        {
            return await _productRepository.GetByIdAsync(1,CancellationToken.None);
        }
    }
}
