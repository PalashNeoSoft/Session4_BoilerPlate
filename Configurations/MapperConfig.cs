using AutoMapper;
using Session4_BoilerPlate.AddDbContext;
using Session4_BoilerPlate.Models;

namespace Session4_BoilerPlate.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
        }
    }
}
