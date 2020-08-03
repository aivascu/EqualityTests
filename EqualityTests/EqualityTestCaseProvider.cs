using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Kernel;

namespace EqualityTests
{
    public class EqualityTestCaseProvider : IEqualityTestCaseProvider
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public EqualityTestCaseProvider(ISpecimenBuilder specimenBuilder)
        {
            if (specimenBuilder == null)
            {
                throw new ArgumentNullException("specimenBuilder");
            }
            this.specimenBuilder = specimenBuilder;
        }

        public IEnumerable<EqualityTestCase> For(Type type)
        {
            var tracker = new ConstructorArgumentsTracker(specimenBuilder, type.GetConstructors().Single());

            var instance = tracker.CreateNewInstance();
            var anotherInstance = tracker.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            yield return new EqualityTestCase(instance, anotherInstance, true);

            foreach (var distinctInstance in tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance))
            {
                yield return new EqualityTestCase(instance, distinctInstance, false);
            }
        }
    }
}