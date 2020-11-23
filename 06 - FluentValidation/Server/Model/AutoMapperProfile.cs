using AutoMapper;
using ConfTool.Shared.DTO;

namespace ConfTool.Server.Model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Conference, ConferenceOverview>();
            CreateMap<ConferenceOverview, Conference>();

            CreateMap<Model.Conference, ConferenceDetails>();
            CreateMap<Model.Conference, ConferenceOverview>();
            CreateMap<ConferenceDetails, Conference>();
            CreateMap<ConferenceOverview, Conference>();
        }
    }
}
