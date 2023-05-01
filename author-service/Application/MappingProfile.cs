using author_service.Models;
using AutoMapper;

namespace author_service.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AuthorBook, AuthorDto>();
        }
    }
}