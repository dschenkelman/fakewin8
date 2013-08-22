namespace FakeWin8.Tests
{
    using System;
    using System.Linq;

    using FakeWin8.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class FakeActionTests
    {
        [TestClass]
        public class OneParameter
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action<object> action = o => { actionCalled = true; };
                object ignored = null;

                var fakeMethod = FakeMethod.CreateFor(action);

                fakeMethod.Invoke(ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                object ignored = null;
                var fakeMethod = FakeMethod.CreateFor<object>(o => { });

                fakeMethod.Invoke(ignored);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored);
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored);
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToActionWhenFakeActionIsInvoked()
            {
                object o = new object();

                var fakeMethod = FakeMethod.CreateFor<object>(p => { });

                fakeMethod.Invoke(o);

                Assert.AreSame(o, fakeMethod.Invocations.First().Parameter);
            }

            [TestMethod]
            public void ShouldKeepTrackOfParametersForConsecutiveInvocations()
            {
                var first = new object();
                var second = new object();
                var third = new object();

                var fakeMethod = FakeMethod.CreateFor<object>(p => { });

                fakeMethod.Invoke(first);
                fakeMethod.Invoke(second);
                fakeMethod.Invoke(third);

                Assert.AreSame(first, fakeMethod.Invocations.ElementAt(0).Parameter);
                Assert.AreSame(second, fakeMethod.Invocations.ElementAt(1).Parameter);
                Assert.AreSame(third, fakeMethod.Invocations.ElementAt(2).Parameter);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptedConditionValues()
            {
                var fakeMethod = FakeMethod.CreateFor<int>(p => { }).Accept(n => n == 2);

                fakeMethod.Invoke(3);
            }

            [TestMethod]
            public void ShouldInvokeActionIfAcceptConditionIsMet()
            {
                bool invoked = false;

                var fakeMethod = FakeMethod.CreateFor<int>(p => { invoked = true; }).Accept(n => n == 2);

                fakeMethod.Invoke(2);

                Assert.IsTrue(invoked);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);
                Assert.AreEqual(2, fakeMethod.Invocations.First().Parameter);
            }
        }

    }
}
