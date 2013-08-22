namespace FakeWin8.Generator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FakeTypeSignatureGeneratorTests
    {
        private interface IInterface
        {
        }

        private interface IGenericInterface<T>
        {
        }

        private interface IGenericInterfaceWithDefaultConstructorConstraint<T> where T : new()
        {
        }

        private interface IGenericInterfaceWithClassConstraint<T> where T : class
        {
        }

        private interface IGenericInterfaceWithStructConstraint<T> where T : struct
        {
        }

        private interface IGenericInterfaceWithOtherArgumentConstraint<T, U> where T : U
        {
        }

        private abstract class AbstractClass
        {
        }
        
        private abstract class AbstractGenericClass<T>
        {
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForInterfaceFake()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IInterface));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeInterface : IInterface", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForClassFake()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(AbstractClass));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeAbstractClass : AbstractClass", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceFake()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterface<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterface<T> : IGenericInterface<T>", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForAbstractGenericClassFake()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(AbstractGenericClass<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeAbstractGenericClass<T> : AbstractGenericClass<T>", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithDefaultConstructorTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithDefaultConstructorConstraint<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithDefaultConstructorConstraint<T> : IGenericInterfaceWithDefaultConstructorConstraint<T> where T : new()", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithClassTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithClassConstraint<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithClassConstraint<T> : IGenericInterfaceWithClassConstraint<T> where T : class", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithStructTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithStructConstraint<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithStructConstraint<T> : IGenericInterfaceWithStructConstraint<T> where T : struct", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithOtherArgumentTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithOtherArgumentConstraint<,>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithOtherArgumentConstraint<T, U> : IGenericInterfaceWithOtherArgumentConstraint<T, U> where T : U", signature);
        }
    }
}