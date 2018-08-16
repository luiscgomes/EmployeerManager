using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.ValueOjects;
using EmployeeManager.UnitTests.AutoFixture;
using System;
using Xunit;

namespace EmployeeManager.UnitTests.Domain.Entities
{
    public class EmployeeTests
    {
        [Fact]
        public void Init_When_Id_Is_Less_Than_Zero_Should_Throw_Exception()
        {
            var invalidId = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Employee(
                invalidId,
                "Teste",
                new Email("teste@teste.com.br"),
                "TI"));
        }

        [Theory]
        [AutoInlineData("")]
        [AutoInlineData(null)]
        public void Init_When_Name_Is_Null_Or_Empty_Should_Throw_Exception(string name)
        {
            Assert.Throws<ArgumentException>(() => new Employee(
                1,
                name,
                new Email("teste@teste.com.br"),
                "TI"));
        }

        [Fact]
        public void Init_When_Email_Is_Null_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new Employee(
                1,
                "Teste",
                null,
                "TI"));
        }

        [Theory]
        [AutoInlineData("")]
        [AutoInlineData(null)]
        public void Init_When_Department_Is_Null_Or_Empty_Should_Throw_Exception(string department)
        {
            Assert.Throws<ArgumentException>(() => new Employee(
                1,
                "Teste",
                new Email("teste@teste.com.br"),
                department));
        }
    }
}