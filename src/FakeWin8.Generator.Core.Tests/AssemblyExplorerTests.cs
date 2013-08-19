namespace FakeWin8.Generator.Core.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AssemblyExplorerTests
    {
        [TestMethod]
        public void ShouldReturnAbstractAndInterfaceTypes()
        {
            var explorer = new AssemblyExplorer(new[] { typeof(IInterface), typeof(AbstractClass), typeof(ConcreteClass) });

            var fakeableTypes = explorer.GetFakeableTypes().ToList();

            Assert.AreEqual(2, fakeableTypes.Count);
            CollectionAssert.Contains(fakeableTypes, typeof(IInterface));
            CollectionAssert.Contains(fakeableTypes, typeof(AbstractClass));
        }

        private interface IInterface
        {
        }

        private abstract class AbstractClass : IInterface
        {
        }

        private class ConcreteClass : AbstractClass
        {
        }
    }
}
