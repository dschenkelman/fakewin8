namespace FakeWin8.Generator.Core.Tests
{
    using System.Linq;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeExplorerTests
    {
        [TestMethod]
        public void ShouldOnlyReturnPublicAbstractMethodsWithLessThanFourParameters()
        {
            var explorer = new TypeExplorer(typeof(HelperClass));

            var methods = explorer.GetFakeableMethods();

            const int ExpectedMethods = 4;

            var methodInfos = methods as MethodInfo[] ?? methods.ToArray();
            Assert.AreEqual(ExpectedMethods, methodInfos.Length);

            for (int i = 1; i <= ExpectedMethods; i++)
            {
                Assert.IsNotNull(methodInfos.SingleOrDefault(m => m.Name == string.Format("Method{0}", i)));
            }
        }

        private abstract class HelperClass
        {
            public abstract void Method1();
            
            public abstract string Method2(int p1);
            
            public abstract double Method3(int p1, int p2);
            
            public abstract int Method4(int p1, int p2, int p3);
            
            // to be excluded, 4 parameters
            public abstract void Method5(int p1, int p2, int p3, int p4);
            
            // to be excluded, 5 parameters
            public abstract void Method6(int p1, int p2, int p3, int p4, int p5);

            // to be excluded, protected
            protected abstract void Method7();

            // to be excluded, abstract
            public void Method8()
            {
            }
        }
    }
}
