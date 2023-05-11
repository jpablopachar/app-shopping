using AutoMapper;
using book_service.Models;

namespace book_service.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LibraryMaterial, LibraryMaterialDto>();
        }
    }
}