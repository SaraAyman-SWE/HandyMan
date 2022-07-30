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
            //Client DTO
            CreateMap<ClientDto, Client>();

            CreateMap<Client, ClientDto>();


            //Handyman Dto

            CreateMap<HandymanDto, Handyman>();

            CreateMap<Handyman, HandymanDto>();


            //Request Dto

            CreateMap<Request, RequestDto>();

            CreateMap<RequestDto, Request>();


            //Payment Dto

            CreateMap<Payment, PaymentDto>();

            CreateMap<PaymentDto, Payment>();


            //Region Dto

            CreateMap<Region, RegionDto>();

            CreateMap<RegionDto, Region>();

            CreateMap<Region, RegionAdminDto>();

            CreateMap<RegionAdminDto, Region>();


            //Craft DTO
            CreateMap<CraftDto, Craft>();

            CreateMap<Craft, CraftDto>();

            //Schedule DTO
            CreateMap<ScheduleDto, Schedule>();

            CreateMap<Schedule, ScheduleDto>();
        }
    }
}
