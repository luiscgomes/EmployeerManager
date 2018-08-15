using EmployeeManager.Api.Mappers;
using Xunit;

namespace EmployeeManager.UnitTests
{
    [CollectionDefinition("UseAutoMapper")]
    public class AutoMapperCollection : ICollectionFixture<AutoMapperConfigurationFixture>
    {
    }

    internal class AutoMapperConfigurationFixture
    {
        public AutoMapperConfigurationFixture()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}