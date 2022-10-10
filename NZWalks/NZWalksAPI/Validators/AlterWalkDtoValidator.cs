using FluentValidation;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;
using System.Runtime.CompilerServices;

namespace NZWalksAPI.Validators
{
    public class AlterWalkDtoValidator : AbstractValidator<AlterWalkDto>
    {
        public AlterWalkDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
