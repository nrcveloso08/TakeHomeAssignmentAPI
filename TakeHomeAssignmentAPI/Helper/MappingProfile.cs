using AutoMapper;
using TakeHomeAssignmentAPI.DTO;
using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Packaging, PackageDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<PackageDTO, Packaging>().ReverseMap();
            
        }
    }
}
