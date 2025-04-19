using FluentValidation;
using UniversityApp.Models;

namespace UniversityApp.Validators
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(100)
                .MustAsync(async (name, ct, ctx) =>
                {
                    // Not checking DB here; optional
                    return true;
                });
        }
    }
}
