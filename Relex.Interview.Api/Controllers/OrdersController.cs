using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Relex.Interview.Api.Dtos.Order;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Batch> _batchRepository;
        private readonly IProductBatchRepository _productBatchRepository;
        private readonly IMapper _mapper;
        public OrdersController(IRepository<Order> orderRepository,
            IRepository<Batch> batchRepository,
            IProductBatchRepository productBatchRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _batchRepository = batchRepository;
            _productBatchRepository = productBatchRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDto>> Get(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<OrderDto> Get(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id, CancellationToken.None).ConfigureAwait(false);
            var result = _mapper.Map<OrderDto>(order);
            return result;
        }

        [HttpPost]
        public async Task<Order> Create([FromBody] CreateOrderDto dto, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(dto);
            order.BatchId = PrepareBatchId(dto);

            await _orderRepository.AddAsync(order, cancellationToken).ConfigureAwait(false);
            await _orderRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return order;
        }

        private int PrepareBatchId(CreateOrderDto dto)
        {
            int batchId = 0;
            var productBatches = GetOrderedBatches(dto.ProductId);
            if (productBatches.Count > 0)
            {
                batchId = dto.IsBatchMaxSize ? productBatches.Last() : productBatches.First();
            }
            else
            {
                batchId = GenerateDefaultBatchForProduct(dto);
            }

            return batchId;
        }

        private List<int> GetOrderedBatches(int productId)
        {
            return _productBatchRepository.GetBatchesByProductId(productId)
                            .OrderBy(x => x.Size)
                            .Select(x => x.Id)
                            .ToList();
        }

        private int GenerateDefaultBatchForProduct(CreateOrderDto dto)
        {
            int batchId;
            var defaultBatchCode = $"B_GENERATED_{dto.ProductId}";
            var defaultBatch = _batchRepository.TableNoTracking.SingleOrDefault(i => i.Code.Equals(defaultBatchCode));
            bool hasDefaultBatch = defaultBatch != null;
            if (hasDefaultBatch)
            {
                batchId = defaultBatch.Id;
            }
            else
            {
                _batchRepository.Add(new Batch
                {
                    Code = defaultBatchCode,
                    Size = 1
                });
                _batchRepository.SaveChanges();
                var newGeneratedBatch = _batchRepository.TableNoTracking.SingleOrDefault(i => i.Code.Equals(defaultBatchCode));
                batchId = newGeneratedBatch.Id;
            }

            return batchId;
        }

        [HttpDelete("{id}")]
        public async Task<Order> Delete(int id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            await _orderRepository.DeleteAsync(order, cancellationToken).ConfigureAwait(false);
            await _orderRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return order;
        }

        [HttpPut("{id}")]
        public async Task<Order> Update(int id, [FromBody] EditOrderDto dto, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            var updatedOrder = _mapper.Map(dto, order);
            await _orderRepository.UpdateAsync(updatedOrder, cancellationToken).ConfigureAwait(false);
            await _orderRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return updatedOrder;
        }
    }
}
