using EmployeeManager.Domain.ValueOjects;
using System;

namespace EmployeeManager.Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Email Email { get; private set; }

        public string Department { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? DeletedAt { get; private set; }

        public Employee(
            int id,
            string name,
            Email email,
            string department)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Id must be greater than 0");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty or null", nameof(name));

            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Department cannot be empty or null", nameof(department));

            Id = id;
            Name = name;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Department = department;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetToDeleted()
        {
            DeletedAt = DateTime.UtcNow;
        }
    }
}