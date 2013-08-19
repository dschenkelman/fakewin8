namespace FakeWin8.Generator.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FakeMethodImplementationGeneratorTests
    {
        private interface IHelper
        {
            void NoParametersReturnsVoid();

            void OneSimpleParameterReturnsVoid(int p1);

            void ThreeGenericParametersReturnsVoid(IEnumerable<int> p1, Task<double> p2, Tuple<char, string> p3);

            int NoParametersReturnsInt();

            string OneSimpleParameterReturnsString(int p1);

            Tuple<Task<string>, double> ThreeGenericParametersReturnsGeneric(IEnumerable<int> p1, Task<double> p2, Tuple<char, string> p3);
        }

        [TestMethod]
        public void ShoulGenerateImplementationForMethodWithNoParametersThatReturnsVoid()
        {
            const string MethodName = "NoParametersReturnsVoid";

            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);
            var generator = new FakeMethodImplementationGenerator(method);

            var methodImplementation = generator.Generate();

            string expectedImplementation = 
                "public void NoParametersReturnsVoid()" 
                + Environment.NewLine 
                + "{"
                + Environment.NewLine 
                + "this.NoParametersReturnsVoidAction.Invoke();"
                + Environment.NewLine 
                + "}";

            Assert.AreEqual(expectedImplementation, methodImplementation);
        }

        [TestMethod]
        public void ShoulGenerateImplementationForMethodWithOneSimpleParameterThatReturnsVoid()
        {
            const string MethodName = "OneSimpleParameterReturnsVoid";

            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);
            var generator = new FakeMethodImplementationGenerator(method);

            var methodImplementation = generator.Generate();

            string expectedImplementation =
                "public void OneSimpleParameterReturnsVoid(int p1)"
                + Environment.NewLine
                + "{"
                + Environment.NewLine
                + "this.OneSimpleParameterReturnsVoidAction.Invoke(p1);"
                + Environment.NewLine
                + "}";

            Assert.AreEqual(expectedImplementation, methodImplementation);
        }

        [TestMethod]
        public void ShoulGenerateImplementationForMethodWithThreeGenericParametersThatReturnsVoid()
        {
            const string MethodName = "ThreeGenericParametersReturnsVoid";

            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);
            var generator = new FakeMethodImplementationGenerator(method);

            var methodImplementation = generator.Generate();

            string expectedImplementation =
                "public void ThreeGenericParametersReturnsVoid(IEnumerable<int> p1, Task<double> p2, Tuple<char, string> p3)"
                + Environment.NewLine
                + "{"
                + Environment.NewLine
                + "this.ThreeGenericParametersReturnsVoidAction.Invoke(p1, p2, p3);"
                + Environment.NewLine
                + "}";

            Assert.AreEqual(expectedImplementation, methodImplementation);
        }

        [TestMethod]
        public void ShoulGenerateImplementationForMethodWithNoParametersThatReturnsInt()
        {
            const string MethodName = "NoParametersReturnsInt";

            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);
            var generator = new FakeMethodImplementationGenerator(method);

            var methodImplementation = generator.Generate();

            string expectedImplementation =
                "public int NoParametersReturnsInt()"
                + Environment.NewLine
                + "{"
                + Environment.NewLine
                + "return this.NoParametersReturnsIntFunc.Invoke();"
                + Environment.NewLine
                + "}";

            Assert.AreEqual(expectedImplementation, methodImplementation);
        }

        [TestMethod]
        public void ShoulGenerateImplementationForMethodWithOneSimpleParameterThatReturnsString()
        {
            const string MethodName = "OneSimpleParameterReturnsString";

            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);
            var generator = new FakeMethodImplementationGenerator(method);

            var methodImplementation = generator.Generate();

            string expectedImplementation =
                "public string OneSimpleParameterReturnsString(int p1)"
                + Environment.NewLine
                + "{"
                + Environment.NewLine
                + "return this.OneSimpleParameterReturnsStringFunc.Invoke(p1);"
                + Environment.NewLine
                + "}";

            Assert.AreEqual(expectedImplementation, methodImplementation);
        }

        [TestMethod]
        public void ShoulGenerateImplementationForMethodWithThreeGenericParametersThatReturnsGenericType()
        {
            const string MethodName = "ThreeGenericParametersReturnsGeneric";

            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);
            var generator = new FakeMethodImplementationGenerator(method);

            var methodImplementation = generator.Generate();

            string expectedImplementation =
                "public Tuple<Task<string>, double> ThreeGenericParametersReturnsGeneric(IEnumerable<int> p1, Task<double> p2, Tuple<char, string> p3)"
                + Environment.NewLine
                + "{"
                + Environment.NewLine
                + "return this.ThreeGenericParametersReturnsGenericFunc.Invoke(p1, p2, p3);"
                + Environment.NewLine
                + "}";

            Assert.AreEqual(expectedImplementation, methodImplementation);
        }
    }
}
