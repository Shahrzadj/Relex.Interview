using AutoMapper;
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
        public async Task<IEnumerable<ProductDto>> Get(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return result;
        }

        [HttpGet("{id}")]   
        public async Task<ProductDto> Get(int id)
        {
            var product= await _productRepository.GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false);
            var result = _mapper.Map<ProductDto>(product);
            return result;
        }

        [HttpPost]
        public async Task<Product> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product, cancellationToken).ConfigureAwait(false);
            await _productRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return product;
        }

        [HttpDelete("{id}")]
        public async Task<Product> Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            await _productRepository.DeleteAsync(product, cancellationToken).ConfigureAwait(false);
            await _productRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return product;
        }

        [HttpPut("{id}")]
        public async Task<Product> Update(int id, [FromBody] EditProductDto dto, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id,cancellationToken).ConfigureAwait(false);
            var updatedProduct = _mapper.Map(dto, product);
            await _productRepository.UpdateAsync(updatedProduct, cancellationToken).ConfigureAwait(false);
            await _productRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return updatedProduct;
        }
    }
}
