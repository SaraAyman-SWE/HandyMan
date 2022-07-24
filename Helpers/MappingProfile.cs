using AutoMapper;
using System.Linq;
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

            CreateMap<Request, RequestDto>();

            CreateMap<RequestDto, Request>();
        }
    }
}
