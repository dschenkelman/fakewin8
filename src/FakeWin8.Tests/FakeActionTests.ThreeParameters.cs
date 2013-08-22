namespace FakeWin8.Tests
{
    using System;
    using System.Linq;

    using FakeWin8.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public partial class FakeActionTests
    {
        [TestClass]
        public class ThreeParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action<object, object, object> action = (o1, o2, o3) => { actionCalled = true; };
                object ignored = null;

                var fakeMethod = FakeMethod.CreateFor(action);

                fakeMethod.Invoke(ignored, ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                object ignored = null;
                var fakeMethod = FakeMethod.CreateFor<object, object, object>((o1, o2, o3) => { });

                fakeMethod.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToActionWhenFakeActionIsInvoked()
            {
                object param1 = new object();
                object param2 = new object();
                object param3 = new object();

                var fakeMethod = FakeMethod.CreateFor<object, object, object>((o1, o2, o3) => { });

                fakeMethod.Invoke(param1, param2, param3);

                Assert.AreSame(param1, fakeMethod.Invocations.First().FirstParameter);
                Assert.AreSame(param2, fakeMethod.Invocations.First().SecondParameter);
                Assert.AreSame(param3, fakeMethod.Invocations.First().ThirdParameter);
            }

            [TestMethod]
            public void ShouldKeepTrackOfParametersForConsecutiveInvocations()
            {
                var first1 = new object();
                var second1 = new object();
                var third1 = new object();

                var first2 = new object();
                var second2 = new object();
                var third2 = new object();

                var first3 = new object();
                var second3 = new object();
                var third3 = new object();

                var fakeMethod = FakeMethod.CreateFor<object, object, object>((o1, o2, o3) => { });

                fakeMethod.Invoke(first1, first2, first3);
                fakeMethod.Invoke(second1, second2, second3);
                fakeMethod.Invoke(third1, third2, third3);

                Assert.AreSame(first1, fakeMethod.Invocations.ElementAt(0).FirstParameter);
                Assert.AreSame(first2, fakeMethod.Invocations.ElementAt(0).SecondParameter);
                Assert.AreSame(first3, fakeMethod.Invocations.ElementAt(0).ThirdParameter);

                Assert.AreSame(second1, fakeMethod.Invocations.ElementAt(1).FirstParameter);
                Assert.AreSame(second2, fakeMethod.Invocations.ElementAt(1).SecondParameter);
                Assert.AreSame(second3, fakeMethod.Invocations.ElementAt(1).ThirdParameter);

                Assert.AreSame(third1, fakeMethod.Invocations.ElementAt(2).FirstParameter);
                Assert.AreSame(third2, fakeMethod.Invocations.ElementAt(2).SecondParameter);
                Assert.AreSame(third3, fakeMethod.Invocations.ElementAt(2).ThirdParameter);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptedConditionDueToFirstParameter()
            {
                var fakeMethod = FakeMethod
                    .CreateFor<int, int, int>((p1, p2, p3) => { })
                    .Accept(n => n == 1, n => n == 2, n => n == 3);

                fakeMethod.Invoke(2, 2, 3);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptanceConditionDueToSecondParameter()
            {
                var fakeMethod = FakeMethod
                    .CreateFor<int, int, int>((p1, p2, p3) => { })
                    .Accept(n => n == 1, n => n == 2, n => n == 3);

                fakeMethod.Invoke(1, 1, 3);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptanceConditionDueToThirdParameter()
            {
                var fakeMethod = FakeMethod
                    .CreateFor<int, int, int>((p1, p2, p3) => { })
                    .Accept(n => n == 1, n => n == 2, n => n == 3);

                fakeMethod.Invoke(1, 2, 2);
            }

            [TestMethod]
            public void ShouldInvokeActionIfParametersMeetAcceptanceCondition()
            {
                bool actionInvoked = false;
                var fakeMethod = FakeMethod.CreateFor<int, int, int>((p1, p2, p3) => { actionInvoked = true; }).Accept(n => n == 1, n => n == 2, n => n == 3);

                fakeMethod.Invoke(1, 2, 3);

                Assert.IsTrue(actionInvoked);

                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);
                Assert.AreEqual(1, fakeMethod.Invocations.First().FirstParameter);
                Assert.AreEqual(2, fakeMethod.Invocations.First().SecondParameter);
                Assert.AreEqual(3, fakeMethod.Invocations.First().ThirdParameter);
            }
        }
    }
}
