namespace EmployeeManager.Api.Contracts
{
    public class PageModel
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 25;
    }
}