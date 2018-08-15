using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using EmployeeManager.Api.Contracts;
using EmployeeManager.Domain.ValueOjects;
using System;
using System.Linq;

namespace EmployeeManager.UnitTests.AutoFixture
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : this(FixtureFactory)
        {
        }

        public AutoNSubstituteDataAttribute(Func<IFixture> fixtureFactory)
            : base(fixtureFactory)
        {
        }

        public static IFixture FixtureFactory()
        {
            var fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
            fixture.RepeatCount = 1;

            fixture.Register(() => new EmployeeCreateModel
            {
                Email = "teste@teste.com"
            });

            fixture.Register(() => new Email("teste@teste.com"));

            return fixture;
        }
    }
}