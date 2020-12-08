using AutoMapper;
using BlazorWasmVirtualization.Shared.DTO;

namespace BlazorWasmVirtualization.Server.Model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Conference, ConferenceOverview>();
            CreateMap<ConferenceOverview, Conference>();

            CreateMap<Conference, ConferenceDetails>();
            CreateMap<Conference, ConferenceOverview>();
            CreateMap<ConferenceDetails, Conference>();
            CreateMap<ConferenceOverview, Conference>();
        }
    }
}
