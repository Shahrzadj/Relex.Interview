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
        private readonly IMapper _mapper;
        public OrdersController(IRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
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
            var order = await _orderRepository.GetByIdAsync(1, CancellationToken.None).ConfigureAwait(false);
            var result = _mapper.Map<OrderDto>(order);
            return result;
        }
    }
}
