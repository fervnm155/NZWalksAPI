using FluentValidation;
using NZWalksAPI.Models.DTOs;

namespace NZWalksAPI.Validators
{
    public class AlterRegionDtoValidator : AbstractValidator<AlterRegionDto>
    {
        public AlterRegionDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Lat).GreaterThan(0);
            RuleFor(x => x.Long).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
