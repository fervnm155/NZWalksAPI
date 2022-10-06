using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class RegionDtoProfile: Profile
    {
        public RegionDtoProfile()
        {
            CreateMap<Models.Domain.Region,Models.DTOs.RegionDto>().ReverseMap();
        }
    }
}
