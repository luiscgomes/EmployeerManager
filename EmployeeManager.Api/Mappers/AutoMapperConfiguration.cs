using EmployeeManager.Api.Mappers.Profiles;

namespace EmployeeManager.Api.Mappers
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}