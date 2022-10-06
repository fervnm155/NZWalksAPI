using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class WalkProfile: Profile
    {
        public WalkProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTOs.WalkDto>().ReverseMap();

            CreateMap<Models.Domain.WalkDifficulty, Models.DTOs.WalkDifficultyDto>().ReverseMap();
        }
    }
}
