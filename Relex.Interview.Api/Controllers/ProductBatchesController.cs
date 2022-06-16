using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relex.Interview.Api.Dtos.ProductBatch;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBatchesController : ControllerBase
    {
        private readonly IRepository<ProductBatch> _productBatchRepository;
        private readonly IMapper _mapper;
        public ProductBatchesController(IRepository<ProductBatch> productBatchRepository, IMapper mapper)
        {
            _productBatchRepository = productBatchRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductBatchDto>> Get(CancellationToken cancellationToken)
        {
            var products = await _productBatchRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<ProductBatchDto>>(products);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ProductBatchDto> Get(int id)
        {
            var product = await _productBatchRepository.GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false);
            var result = _mapper.Map<ProductBatchDto>(product);
            return result;
        }

        [HttpPost]
        public async Task<ProductBatch> Create([FromBody] CreateProductBatchDto dto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductBatch>(dto);
            await _productBatchRepository.AddAsync(product, cancellationToken).ConfigureAwait(false);
            await _productBatchRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return product;
        }

        [HttpDelete("{id}")]
        public async Task<ProductBatch> Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _productBatchRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            await _productBatchRepository.DeleteAsync(product, cancellationToken).ConfigureAwait(false);
            await _productBatchRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return product;
        }

        [HttpPut("{id}")]
        public async Task<ProductBatch> Update(int id, [FromBody] EditProductBatchDto dto, CancellationToken cancellationToken)
        {
            var product = await _productBatchRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            var updatedProduct = _mapper.Map(dto, product);
            await _productBatchRepository.UpdateAsync(updatedProduct, cancellationToken).ConfigureAwait(false);
            await _productBatchRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return updatedProduct;
        }
    }
}
