using EmployeeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManager.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Department).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeletedAt);

            builder.OwnsOne(x => x.Email, f =>
            {
                f.Property(x => x.Address)
                    .IsRequired()
                    .HasColumnName("Email");
            });

            builder.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}