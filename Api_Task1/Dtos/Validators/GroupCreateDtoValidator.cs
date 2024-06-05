using Api_Task1.Dtos.GroupDtos;
using FluentValidation;

namespace Api_Task1.Dtos.Validators
{
    public class GroupCreateDtoValidator : AbstractValidator<GroupCreateDto>
    {
        public GroupCreateDtoValidator()
        {
            RuleFor(x => x.No).NotEmpty().MinimumLength(4).MaximumLength(5);
            RuleFor(x => (int)x.Limit).NotNull().InclusiveBetween(5, 18);
        }
    }
}
