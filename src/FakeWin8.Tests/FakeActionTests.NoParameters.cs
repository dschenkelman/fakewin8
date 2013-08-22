namespace FakeWin8.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class FakeActionTests
    {
        [TestClass]
        public class NoParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action action = () => { actionCalled = true; };

                var fakeMethod = FakeMethod.CreateFor(action);

                fakeMethod.Invoke();

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                var fakeMethod = FakeMethod.CreateFor(() => { });

                fakeMethod.Invoke();
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke();
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke();
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }
        }
    }
}