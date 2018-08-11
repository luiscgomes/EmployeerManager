using EmployeeManager.Api.Contracts;
using FluentValidation;

namespace EmployeeManager.Api.Validations
{
    public class PageModelValidator : AbstractValidator<PageModel>
    {
        public PageModelValidator()
        {
            RuleFor(p => p.PageSize)
                .LessThanOrEqualTo(100)
                .WithMessage("Page Size is too big")
                .GreaterThan(0)
                .WithMessage("Page Size is too small");

            RuleFor(p => p.Page)
                .GreaterThan(0)
                .WithMessage("Page is too small");
        }
    }
}