namespace FakeWin8.Generator.Core.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeGeneratorTests
    {
        [TestMethod]
        public void ShouldCorrectlyGenerateTypeForSimpleAliasedType()
        {
            var generator = new TypeGenerator(typeof(int));

            Assert.AreEqual("int", generator.Generate());
        }

        [TestMethod]
        public void ShouldCorrectlyGenerateTypeForSimpleNonAliasedType()
        {
            var generator = new TypeGenerator(typeof(DateTime));

            Assert.AreEqual("DateTime", generator.Generate());
        }

        [TestMethod]
        public void ShouldCorrectlyGenerateTypeForGenericTypeWithOneGenericParameter()
        {
            var generator = new TypeGenerator(typeof(Func<double>));

            Assert.AreEqual("Func<double>", generator.Generate());
        }

        [TestMethod]
        public void ShouldCorrectlyGenerateTypeForGenericTypeWithMultipleGenericParameters()
        {
            var generator = new TypeGenerator(typeof(Func<double, int, string, DateTime>));

            Assert.AreEqual("Func<double, int, string, DateTime>", generator.Generate());
        }

        [TestMethod]
        public void ShouldCorrectlyGenerateTypeForRecursiveGenericTypesWithOneGenericParameterEach()
        {
            var generator = new TypeGenerator(typeof(Func<Tuple<int>>));

            Assert.AreEqual("Func<Tuple<int>>", generator.Generate());
        }

        [TestMethod]
        public void ShouldCorrectlyGenerateTypeForRecursiveGenericTypesWithMultipleGenericParametersEach()
        {
            var generator = new TypeGenerator(typeof(Func<Tuple<int, int, int>, Tuple<double, double, double>, Tuple<string, string, Tuple<short, short>>>));

            Assert.AreEqual("Func<Tuple<int, int, int>, Tuple<double, double, double>, Tuple<string, string, Tuple<short, short>>>", generator.Generate());
        }
    }
}
