using AutoMapper;
using HandyMan.Dtos;
using HandyMan.Models;

namespace HandyMan.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientDto, Client>();
            CreateMap<Client, ClientDto>();
        }
    }
}
