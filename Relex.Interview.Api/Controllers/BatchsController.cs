using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Relex.Interview.Api.Dtos.Batch;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchsController : ControllerBase
    {
        private readonly IRepository<Batch> _batchRepository;
        private readonly IMapper _mapper;
        public BatchsController(IRepository<Batch> batchRepository, IMapper mapper)
        {
            _batchRepository = batchRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BatchDto>> Get(CancellationToken cancellationToken)
        {
            var batches = await _batchRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<BatchDto>>(batches);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<BatchDto> Get(int id, CancellationToken cancellationToken)
        {
            var batch = await _batchRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<BatchDto>(batch);
            return result;
        }

        [HttpPost]
        public async Task<Batch> Create([FromBody] CreateBatchDto dto, CancellationToken cancellationToken)
        {
            var batch = _mapper.Map<Batch>(dto);
            await _batchRepository.AddAsync(batch, cancellationToken).ConfigureAwait(false);
            await _batchRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return batch;
        }

        [HttpDelete("{id}")]
        public async Task<Batch> Delete(int id, CancellationToken cancellationToken)
        {
            var batch = await _batchRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            await _batchRepository.DeleteAsync(batch, cancellationToken).ConfigureAwait(false);
            await _batchRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return batch;
        }

        [HttpPut("{id}")]
        public async Task<Batch> Update(int id, [FromBody] EditBatchDto dto, CancellationToken cancellationToken)
        {
            var batch = await _batchRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            var updatedBatch = _mapper.Map(dto, batch);
            await _batchRepository.UpdateAsync(updatedBatch, cancellationToken).ConfigureAwait(false);
            await _batchRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return updatedBatch;
        }
    }
}
