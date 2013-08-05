namespace FakeWin8.Tests
{
    using System;
    using System.Linq;

    using FakeWin8.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class FakeFuncTests
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
                var fakeMethod = FakeMethod.CreateFor<int, int>(p => p).AcceptOnly(n => n == 2);

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
                    }).AcceptOnly(n => n == 2);

                fakeMethod.Invoke(2);

                Assert.IsTrue(invoked);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);
                Assert.AreEqual(2, fakeMethod.Invocations.First().Parameter);
            }
        }

        [TestClass]
        public class TwoParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeFuncIsInvoked()
            {
                bool actionCalled = false;
                Func<object, object, object> func = (o1, o2) =>
                {
                    actionCalled = true;
                    return null;
                };

                object ignored = null;

                var fakeMethod = new FakeFunc<object, object, object>(func);

                fakeMethod.Invoke(ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeFuncIsInvoked()
            {
                Func<object, object, object> func = (o1, o2) => null;

                object ignored = null;

                var fakeMethod = new FakeFunc<object, object, object>(func);

                fakeMethod.Invoke(ignored, ignored);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored, ignored);
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored, ignored);
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToFuncWhenFakeFuncIsInvoked()
            {
                var p1 = new object();
                var p2 = new object();

                var fakeMethod = new FakeFunc<object, object, object>((o1, o2) => null);

                fakeMethod.Invoke(p1, p2);

                Assert.AreSame(p1, fakeMethod.Invocations.First().FirstParameter);
                Assert.AreSame(p2, fakeMethod.Invocations.First().SecondParameter);
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

                var fakeMethod = new FakeFunc<object, object, object>((o1, o2) => null);

                fakeMethod.Invoke(first1, first2);
                fakeMethod.Invoke(second1, second2);
                fakeMethod.Invoke(third1, third2);

                Assert.AreSame(first1, fakeMethod.Invocations.ElementAt(0).FirstParameter);
                Assert.AreSame(first2, fakeMethod.Invocations.ElementAt(0).SecondParameter);

                Assert.AreSame(second1, fakeMethod.Invocations.ElementAt(1).FirstParameter);
                Assert.AreSame(second2, fakeMethod.Invocations.ElementAt(1).SecondParameter);

                Assert.AreSame(third1, fakeMethod.Invocations.ElementAt(2).FirstParameter);
                Assert.AreSame(third2, fakeMethod.Invocations.ElementAt(2).SecondParameter);
            }

            [TestMethod]
            public void ShouldReturnValueFromFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeMethod = new FakeFunc<object, object, object>((o1, o2) => toReturn);

                var returned = fakeMethod.Invoke(null, null);

                Assert.AreSame(toReturn, returned);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptedConditionDueToFirstParameter()
            {
                var fakeMethod = FakeMethod.CreateFor<int, int, int>((p1, p2) => p1 + p2)
                    .AcceptOnly(n => n == 1, n => n == 2);

                fakeMethod.Invoke(2, 2);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidInvocationException))]
            public void ShouldThrowExceptionWhenInvocationDoesNotMeetAcceptanceConditionDueToSecondParameter()
            {
                var fakeMethod =
                    FakeMethod.CreateFor<int, int, int>((p1, p2) => p1 + p2)
                              .AcceptOnly(n => n == 1, n => n == 2);

                fakeMethod.Invoke(1, 1);
            }

            [TestMethod]
            public void ShouldInvokeActionIfParametersMeetAcceptanceCondition()
            {
                bool actionInvoked = false;
                var fakeMethod = FakeMethod.CreateFor<int, int, int>(
                    (p1, p2) =>
                        {
                            actionInvoked = true;
                            return p1 + p2;
                        }).AcceptOnly(n => n == 1, n => n == 2);

                fakeMethod.Invoke(1, 2);

                Assert.IsTrue(actionInvoked);

                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);
                Assert.AreEqual(1, fakeMethod.Invocations.First().FirstParameter);
                Assert.AreEqual(2, fakeMethod.Invocations.First().SecondParameter);
            }
        }

        [TestClass]
        public class ThreeParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeFuncIsInvoked()
            {
                bool actionCalled = false;
                Func<object, object, object, object> func = (o1, o2, o3) =>
                {
                    actionCalled = true;
                    return null;
                };

                object ignored = null;

                var fakeMethod = new FakeFunc<object, object, object, object>(func);

                fakeMethod.Invoke(ignored, ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeFuncIsInvoked()
            {
                Func<object, object, object, object> func = (o1, o2, o3) => null;

                object ignored = null;

                var fakeMethod = new FakeFunc<object, object, object, object>(func);

                fakeMethod.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(1, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(2, fakeMethod.NumberOfInvocations);

                fakeMethod.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(3, fakeMethod.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToFuncWhenFakeFuncIsInvoked()
            {
                var p1 = new object();
                var p2 = new object();
                var p3 = new object();

                var fakeMethod = new FakeFunc<object, object, object, object>((o1, o2, o3) => null);

                fakeMethod.Invoke(p1, p2, p3);

                Assert.AreSame(p1, fakeMethod.Invocations.First().FirstParameter);
                Assert.AreSame(p2, fakeMethod.Invocations.First().SecondParameter);
                Assert.AreSame(p3, fakeMethod.Invocations.First().ThirdParameter);
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

                var fakeMethod = new FakeFunc<object, object, object, object>((o1, o2, o3) => null);

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
            public void ShouldReturnValueFromFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeMethod = new FakeFunc<object, object, object, object>((o1, o2, o3) => toReturn);

                var returned = fakeMethod.Invoke(null, null, null);

                Assert.AreSame(toReturn, returned);
            }
        }
    }
}
