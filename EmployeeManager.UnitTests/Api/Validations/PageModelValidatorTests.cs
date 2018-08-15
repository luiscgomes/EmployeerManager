using EmployeeManager.Api.Contracts;
using EmployeeManager.Api.Validations;
using EmployeeManager.UnitTests.AutoFixture;
using FluentValidation.TestHelper;
using Xunit;

namespace EmployeeManager.UnitTests.Api.Validations
{
    public class PageModelValidatorTests
    {
        [Theory, AutoNSubstituteData]
        public void Validate_When_PageSize_Is_Too_Big_Should_Return_False(
            PageModel pageModel,
            PageModelValidator sut)
        {
            pageModel.PageSize = 150;

            sut.ShouldHaveValidationErrorFor(p => p.PageSize, pageModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_PageSize_Is_Too_Small_Should_Return_False(
            PageModel pageModel,
            PageModelValidator sut)
        {
            pageModel.PageSize = -1;

            sut.ShouldHaveValidationErrorFor(p => p.PageSize, pageModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_PageSize_Is_Valid_Should_Return_True(
            PageModel pageModel,
            PageModelValidator sut)
        {
            pageModel.PageSize = 50;

            sut.ShouldNotHaveValidationErrorFor(p => p.PageSize, pageModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Page_Is_Too_Small_Should_Return_False(
            PageModel pageModel,
            PageModelValidator sut)
        {
            pageModel.Page = -1;

            sut.ShouldHaveValidationErrorFor(p => p.Page, pageModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Page_Is_Valid_Should_Return_True(
            PageModel pageModel,
            PageModelValidator sut)
        {
            pageModel.Page = 2;

            sut.ShouldNotHaveValidationErrorFor(p => p.Page, pageModel);
        }
    }
}