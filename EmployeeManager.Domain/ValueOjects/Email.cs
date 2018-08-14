using System;
using System.Text.RegularExpressions;

namespace EmployeeManager.Domain.ValueOjects
{
    public sealed class Email
    {
        public string Address { get; private set; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Email Address cannot be null or empty", nameof(address));

            if (Validate(address) == false)
                throw new ArgumentException("Email Address is invalid", nameof(address));

            Address = address;
        }

        public bool Validate(string emailAddress)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            var match = regex.Match(emailAddress);

            return match.Success;
        }

        public static bool operator ==(Email email, string emailAddress) => email?.Address == emailAddress;

        public static bool operator !=(Email email, string emailAddress) => (email == emailAddress) == false;
    }
}