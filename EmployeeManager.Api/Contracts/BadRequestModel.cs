using System;

namespace EmployeeManager.Api.Contracts
{
    public class BadRequestModel
    {
        public string Message { get; }

        public BadRequestModel(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}