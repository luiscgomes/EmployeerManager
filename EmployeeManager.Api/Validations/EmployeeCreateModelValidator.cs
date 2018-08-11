using EmployeeManager.Api.Contracts;
using FluentValidation;

namespace EmployeeManager.Api.Validations
{
    public class EmployeeCreateModelValidator : AbstractValidator<EmployeeCreateModel>
    {
        public EmployeeCreateModelValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(e => e.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is invalid");

            RuleFor(e => e.Department)
                .NotEmpty()
                .WithMessage("Department is required");
        }
    }
}