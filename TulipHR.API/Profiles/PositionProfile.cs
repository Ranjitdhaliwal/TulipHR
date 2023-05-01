using AutoMapper;

namespace TulipHR.API.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile() {
            CreateMap<Models.PositionCreationDto, Entities.Position>();
        }
    }
}
