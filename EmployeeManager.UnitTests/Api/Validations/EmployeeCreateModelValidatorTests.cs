using EmployeeManager.Api.Contracts;
using EmployeeManager.Api.Validations;
using EmployeeManager.UnitTests.AutoFixture;
using FluentValidation.TestHelper;
using Xunit;

namespace EmployeeManager.UnitTests.Api.Validations
{
    public class EmployeeCreateModelValidatorTests
    {
        [Theory, AutoNSubstituteData]
        public void Validate_When_Name_Is_Null_Or_Empty_Should_Return_False(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Name = null;

            sut.ShouldHaveValidationErrorFor(e => e.Name, employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Name_Not_Is_Null_Or_Empty_Should_Return_True(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Name = "My name";

            sut.ShouldNotHaveValidationErrorFor(e => e.Name, employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Email_Is_Null_Or_Empty_Should_Return_False(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Email = null;

            sut.ShouldHaveValidationErrorFor(e => e.Email, employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Email_Is_Invalid_Should_Return_False(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Email = "teste";

            sut.ShouldHaveValidationErrorFor(e => e.Email, employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Email_Is_Valid_Should_Return_True(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Email = "teste@teste.com.br";

            sut.ShouldNotHaveValidationErrorFor(e => e.Email, employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Department_Is_Null_Or_Empty_Should_Return_False(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Department = null;

            sut.ShouldHaveValidationErrorFor(e => e.Department, employeeCreateModel);
        }

        [Theory, AutoNSubstituteData]
        public void Validate_When_Department_Not_Is_Null_Or_Empty_Should_Return_True(
            EmployeeCreateModel employeeCreateModel,
            EmployeeCreateModelValidator sut)
        {
            employeeCreateModel.Department = "My Department";

            sut.ShouldNotHaveValidationErrorFor(e => e.Department, employeeCreateModel);
        }
    }
}