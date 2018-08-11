namespace EmployeeManager.Api.Mappers.Extensions
{
    public static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object source)
        {
            return AutoMapper.Mapper.Map<T>(source);
        }
    }
}