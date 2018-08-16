using EmployeeManager.Domain.ValueOjects;
using EmployeeManager.UnitTests.AutoFixture;
using FluentAssertions;
using System;
using Xunit;

namespace EmployeeManager.UnitTests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [AutoInlineData("")]
        [AutoInlineData(null)]
        public void Init_When_Address_Is_Null_Or_Empty_Should_Throw_Exception(string address)
        {
            Assert.Throws<ArgumentException>(() => new Email(address));
        }

        [Fact]
        public void Init_When_Address_Is_Invalid_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => new Email("teste"));
        }

        [Theory]
        [AutoInlineData("teste", false)]
        [AutoInlineData("teste@teste.com.br", true)]
        public void Validate_Should_Return_Expected_Value(
            string emailAddress,
            bool expected,
            Email sut)
        {
            var actual = sut.Validate(emailAddress);

            actual.Should().Be(expected);
        }
    }
}