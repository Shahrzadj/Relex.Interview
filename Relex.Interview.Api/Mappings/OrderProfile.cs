using AutoMapper;
using Relex.Interview.Api.Dtos.Order;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Mappings
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, EditOrderDto>().ReverseMap();
        }
    }
}
