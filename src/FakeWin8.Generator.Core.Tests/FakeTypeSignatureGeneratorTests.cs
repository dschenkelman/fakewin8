namespace FakeWin8.Generator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class FakeTypeSignatureGeneratorTests
    {
     
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
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithReferenceTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithReferenceConstraint<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithReferenceConstraint<T> : IGenericInterfaceWithReferenceConstraint<T> where T : class", signature);
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

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithInterfaceTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithInterfaceConstraint<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithInterfaceConstraint<T> : IGenericInterfaceWithInterfaceConstraint<T> where T : IInterface", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithClassTypeConstraint()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithClassConstraint<>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithClassConstraint<T> : IGenericInterfaceWithClassConstraint<T> where T : AbstractClass", signature);
        }

        [TestMethod]
        public void ShouldGenerateFakeTypeSignatureForGenericInterfaceWithMultipleConstraints()
        {
            var generator = new FakeTypeSignatureGenerator(typeof(IGenericInterfaceWithMultipleConstraints<,,,>));

            var signature = generator.Generate();

            Assert.AreEqual("public class FakeGenericInterfaceWithMultipleConstraints<T, U, V, W> : IGenericInterfaceWithMultipleConstraints<T, U, V, W> where T : class, IInterface, W, new() where U : struct, V", signature);
        }
    }
}