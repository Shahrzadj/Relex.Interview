using AutoMapper;
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
        private readonly IMapper _mapper;
        public ProductsController(IRepository<Product> productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _productRepository.GetAllAsync().ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return result;
        }

        [HttpGet("{id}")]   
        public async Task<ProductDto> Get(int id)
        {
            var product= await _productRepository.GetByIdAsync(1, CancellationToken.None).ConfigureAwait(false);
            var result = _mapper.Map<ProductDto>(product);
            return result;
        }
    }
}
