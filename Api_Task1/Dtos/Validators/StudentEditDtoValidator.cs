using Api_Task1.Data;
using Api_Task1.Dtos.StudentDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Api_Task1.Dtos.Validators
{
    public class StudentEditDtoValidator:AbstractValidator<StudentEditDto>
    {
        public StudentEditDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(35)
                .When(x => x.FullName != null);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(100)
                .EmailAddress()
                .When(x => x.Email != null);

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now)
                .When(x=>x.BirthDate != default(DateTime));

            RuleFor(x => x.GroupId)
                .GreaterThan(0)
                .When(x => x.GroupId != 0);
        }
    }
}
