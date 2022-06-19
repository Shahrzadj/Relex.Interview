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
        private readonly IProductBatchRepository _productBatchRepository;
        private readonly IMapper _mapper;
        public OrdersController(IRepository<Order> orderRepository, IProductBatchRepository productBatchRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
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
            var productBatches = _productBatchRepository.GetBatchesByProductId(dto.ProductId)
                .OrderBy(x =>x.Size)
                .Select(x => x.Id)
                .ToList();
            order.BatchId = dto.IsBatchMaxSize ? productBatches.Last() : productBatches.First();
            await _orderRepository.AddAsync(order, cancellationToken).ConfigureAwait(false);
            await _orderRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return order;
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
