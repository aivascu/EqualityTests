﻿using System.Reflection;
using EqualityTests.Assertions;
using AutoFixture;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class EqualsTransitiveAssertionTests
    {
        [Theory, AutoData]
        public void ShouldBeIdiomaticAssertion(EqualsTransitiveAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(EqualsTransitiveAssertion).GetConstructors());
        }

        public class EqualsTransitiveAssertionTests_VerifyMethod
        {
            [Theory, AutoData]
            public void ShouldGuardCheckArguments(
                [Frozen] Fixture fixture,
                GuardClauseAssertion guardClauseAssertion)
            {
                fixture.Inject(typeof (EqualsTransitiveAssertion).GetMethod("Equals", BindingFlags.Public));

                guardClauseAssertion.Verify(typeof (EqualsTransitiveAssertion).GetMethod("Verify",
                    new[] {typeof (MethodInfo)}));
            }
        }
    }
}
