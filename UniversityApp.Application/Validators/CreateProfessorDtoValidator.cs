using FluentValidation;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Validators
{
    public class CreateProfessorDtoValidator : AbstractValidator<CreateProfessorDto>
    {
        public CreateProfessorDtoValidator()
        {
            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100);

            RuleFor(p => p.Department)
                .NotEmpty().WithMessage("Department is required.")
                .MaximumLength(100);

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
