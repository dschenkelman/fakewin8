namespace FakeWin8.Generator.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FakeTypeSignatureGeneratorTests
    {
        private interface IInterface
        {
        }

        private abstract class AbstractClass
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
    }
}