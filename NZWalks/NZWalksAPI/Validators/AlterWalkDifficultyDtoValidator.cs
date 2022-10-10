using FluentValidation;
using NZWalksAPI.Models.DTOs;

namespace NZWalksAPI.Validators
{
    public class AlterWalkDifficultyDtoValidator : AbstractValidator<AlterWalkDifficultyDto>
    {
        public AlterWalkDifficultyDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
