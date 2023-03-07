using AutoMapper;
using VoxTenouApp.Models.SportEvent;

namespace VoxTenouApp
{
    public class MapperProfile : MapperConfigurationExpression
    {
        public MapperProfile()
        {
            CreateMap<GridSportEvent,SubmitSportEventDto>().ReverseMap();
            CreateMap<GridSportEvent, SubmitEditSportEventDto>().ReverseMap();
            CreateMap<SubmitSportEventDto, SubmitEditSportEventDto>().ReverseMap();
        }
    }
}
