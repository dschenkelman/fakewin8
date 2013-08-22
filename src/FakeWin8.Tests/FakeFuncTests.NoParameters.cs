namespace FakeWin8.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class FakeFuncTests
    {
        [TestClass]
        public class NoParameters
        {
            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsWhenFakeFuncIsInvoked()
            {
                var fakeMethod = FakeMethod.CreateFor<object>(() => null);

                fakeMethod.Invoke();
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke();
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke();
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldInvokeFuncWhenFakeFuncIsInvoked()
            {
                bool funcInvoked = false;
                var fakeMethod = FakeMethod.CreateFor<object>(() =>
                    { 
                        funcInvoked = true;
                        return null;
                    });

                fakeMethod.Invoke();

                Assert.IsTrue(funcInvoked);
            }

            [TestMethod]
            public void ShouldReturnObjectReturnedByFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeMethod = FakeMethod.CreateFor(() => toReturn);

                var returned = fakeMethod.Invoke();

                Assert.AreSame(toReturn, returned);
            }    
        }
    }
}
