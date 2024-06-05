using Api_Task1.Dtos.GroupDtos;
using Api_Task1.Dtos.StudentDtos;
using FluentValidation;

namespace Api_Task1.Dtos.Validators
{
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(35);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
        }
    }
}
