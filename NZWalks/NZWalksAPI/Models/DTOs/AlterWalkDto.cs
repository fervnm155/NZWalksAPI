//using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.DTOs
{
    public class AlterWalkDto
    {
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
