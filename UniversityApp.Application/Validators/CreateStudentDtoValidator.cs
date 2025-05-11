using FluentValidation;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.EnrollmentDate).NotEmpty();
        }
    }
}
