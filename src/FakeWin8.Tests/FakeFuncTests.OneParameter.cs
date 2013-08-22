namespace FakeWin8.Tests
{
    using System;
    using System.Linq;

    using FakeWin8.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class FakeFuncTests
    {
        [TestClass]
        public class OneParameter
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeFuncIsInvoked()
            {
                bool funcCalled = false;
                Func<object, object> func = o =>
                {
                    funcCalled = true;
                    return null;
                };

                object ignored = null;

                var fakeMethod = FakeMethod.CreateFor(func);

                fakeMethod.Invoke(ignored);

                Assert.IsTrue(funcCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeFuncIsInvoked()
            {
                Func<object, object> func = o => null;

                object ignored = null;

                var fakeMethod = FakeMethod.CreateFor(func);

                fakeMethod.Invoke(ignored);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored);
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored);
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToFuncWhenFakeFuncIsInvoked()
            {
                var o = new object();

                var fakeMethod = FakeMethod.CreateFor<object, object>(p => null);

                fakeMethod.Invoke(o);

                Assert.AreSame(o, fakeMethod.Invocations.First().Parameter);
            }

            [TestMethod]
            public void ShouldKeepTrackOfParametersForConsecutiveInvocations()
            {
                var first = new object();
                var second = new object();
                var third = new object();

                var fakeMethod = FakeMethod.CreateFor<object, object>(p => null);

                fakeMethod.Invoke(first);
                fakeMethod.Invoke(second);
                fakeMethod.Invoke(third);

                Assert.AreSame(first, fakeMethod.Invocations.ElementAt(0).Parameter);
                Assert.AreSame(second, fakeMethod.Invocations.ElementAt(1).Parameter);
                Assert.AreSame(third, fakeMethod.Invocations.ElementAt(2).Parameter);
            }

            [TestMethod]
            public void ShouldReturnValueFromFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeMethod = FakeMethod.CreateFor<object, object>(p => toReturn);

                var returned = fakeMethod.Invoke(null);

                Assert.AreSame(toReturn, returned);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptedConditionValues()
            {
                var fakeMethod = FakeMethod.CreateFor<int, int>(p => p).Accept(n => n == 2);

                fakeMethod.Invoke(3);
            }

            [TestMethod]
            public void ShouldInvokeFuncIfAcceptConditionIsMet()
            {
                bool invoked = false;

                var fakeMethod = FakeMethod.CreateFor<int, int>(p =>
                {
                    invoked = true;
                    return p;
                }).Accept(n => n == 2);

                fakeMethod.Invoke(2);

                Assert.IsTrue(invoked);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);
                Assert.AreEqual(2, fakeMethod.Invocations.First().Parameter);
            }
        }
    }
}