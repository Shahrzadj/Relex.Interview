using AutoMapper;
using Relex.Interview.Api.Dtos.ProductBatch;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Mappings
{
    public class ProductBatchProfile: Profile
    {
        public ProductBatchProfile()
        {
            CreateMap<ProductBatch, ProductBatchDto>().ReverseMap();
            CreateMap<ProductBatch, CreateProductBatchDto>().ReverseMap();
            CreateMap<ProductBatch, EditProductBatchDto>().ReverseMap();
        }
    }
}
