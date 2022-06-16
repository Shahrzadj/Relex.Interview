using AutoMapper;
using Relex.Interview.Api.Dtos.Batch;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Mappings
{
    public class BatchProfile: Profile
    {
        public BatchProfile()
        {
            CreateMap<Batch, BatchDto>().ReverseMap();
        }
    }
}
