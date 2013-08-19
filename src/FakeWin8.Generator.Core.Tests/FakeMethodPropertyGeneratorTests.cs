namespace FakeWin8.Generator.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FakeMethodPropertyGeneratorTests
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
        public void ShouldGeneratePropertyForMethodReturningVoidAndReceivingNoParameters()
        {
            const string MethodName = "NoParametersReturnsVoid";
            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);

            var generator = new FakeMethodPropertyGenerator(method);

            Assert.AreEqual(string.Format("public FakeAction {0}Action {{ get; set; }}", MethodName), generator.Generate());
        }

        [TestMethod]
        public void ShouldGeneratePropertyForMethodReturningVoidAndReceivingOneSimpleParameter()
        {
            const string MethodName = "OneSimpleParameterReturnsVoid";
            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);

            var generator = new FakeMethodPropertyGenerator(method);

            Assert.AreEqual(string.Format("public FakeAction<int> {0}Action {{ get; set; }}", MethodName), generator.Generate());
        }

        [TestMethod]
        public void ShouldGeneratePropertyForMethodReturningVoidAndReceivingThreeGenericParameters()
        {
            const string MethodName = "ThreeGenericParametersReturnsVoid";
            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);

            var generator = new FakeMethodPropertyGenerator(method);

            Assert.AreEqual(string.Format("public FakeAction<IEnumerable<int>, Task<double>, Tuple<char, string>> {0}Action {{ get; set; }}", MethodName), generator.Generate());
        }

        [TestMethod]
        public void ShouldGeneratePropertyForMethodReturningIntAndReceivingNoParameters()
        {
            const string MethodName = "NoParametersReturnsInt";
            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);

            var generator = new FakeMethodPropertyGenerator(method);

            Assert.AreEqual(string.Format("public FakeFunc<int> {0}Func {{ get; set; }}", MethodName), generator.Generate());
        }

        [TestMethod]
        public void ShouldGeneratePropertyForMethodReturningStringAndReceivingOneSimpleParameter()
        {
            const string MethodName = "OneSimpleParameterReturnsString";
            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);

            var generator = new FakeMethodPropertyGenerator(method);

            Assert.AreEqual(string.Format("public FakeFunc<int, string> {0}Func {{ get; set; }}", MethodName), generator.Generate());
        }

        [TestMethod]
        public void ShouldGeneratePropertyForMethodReturningGenericTypeAndReceivingThreeGenericParameters()
        {
            const string MethodName = "ThreeGenericParametersReturnsGeneric";
            var method = typeof(IHelper).GetMethods().First(m => m.Name == MethodName);

            var generator = new FakeMethodPropertyGenerator(method);

            Assert.AreEqual(string.Format("public FakeFunc<IEnumerable<int>, Task<double>, Tuple<char, string>, Tuple<Task<string>, double>> {0}Func {{ get; set; }}", MethodName), generator.Generate());
        }
    }
}
