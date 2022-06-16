using AutoMapper;
using Relex.Interview.Api.Dtos.Product;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Mappings
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap(); 
            CreateMap<Product, EditProductDto>().ReverseMap(); 
        }
    }
}
