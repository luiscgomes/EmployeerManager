using AutoMapper;
using Xunit;

namespace EmployeeManager.UnitTests.Api.Mappers
{
    [Collection("UseAutoMapper")]
    public class AutoMapperTests
    {
        [Fact]
        public void Assert_AutoMapper_Configuration_Is_Valid()
        {
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}