using AutoMapper;
using book_service.Application;
using book_service.Models;

namespace test_book_service
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibraryMaterial, LibraryMaterialDto>();
        }
    }
}