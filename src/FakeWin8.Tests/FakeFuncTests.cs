namespace FakeWin8.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class FakeFuncTests
    {
        [TestClass]
        public class NoParameters
        {
            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsWhenFakeFuncIsInvoked()
            {
                var fakeFunc = new FakeFunc<object>(() => null);

                fakeFunc.Invoke();
                Assert.AreEqual(1, fakeFunc.NumberOfInvocations);

                fakeFunc.Invoke();
                Assert.AreEqual(2, fakeFunc.NumberOfInvocations);

                fakeFunc.Invoke();
                Assert.AreEqual(3, fakeFunc.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldInvokeFuncWhenFakeFuncIsInvoked()
            {
                bool funcInvoked = false;
                var fakeFunc = new FakeFunc<object>(() =>
                    { 
                        funcInvoked = true;
                        return null;
                    });

                fakeFunc.Invoke();

                Assert.IsTrue(funcInvoked);
            }

            [TestMethod]
            public void ShouldReturnObjectReturnedByFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeFunc = new FakeFunc<object>(() => toReturn);

                var returned = fakeFunc.Invoke();

                Assert.AreSame(toReturn, returned);
            }    
        }

        [TestClass]
        public class OneParameter
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeFuncIsInvoked()
            {
                bool actionCalled = false;
                Func<object, object> func = o =>
                    {
                        actionCalled = true;
                        return null;
                    };

                object ignored = null;

                var fakeAction = new FakeFunc<object, object>(func);

                fakeAction.Invoke(ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeFuncIsInvoked()
            {
                Func<object, object> func = o => null;

                object ignored = null;

                var fakeAction = new FakeFunc<object, object>(func);

                fakeAction.Invoke(ignored);
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored);
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored);
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToFuncWhenFakeFuncIsInvoked()
            {
                var o = new object();

                var fakeAction = new FakeFunc<object, object>(p => null);

                fakeAction.Invoke(o);

                Assert.AreSame(o, fakeAction.Invocations.First().Parameter);
            }

            [TestMethod]
            public void ShouldKeepTrackOfParametersForConsecutiveInvocations()
            {
                var first = new object();
                var second = new object();
                var third = new object();

                var fakeAction = new FakeFunc<object, object>(p => null);

                fakeAction.Invoke(first);
                fakeAction.Invoke(second);
                fakeAction.Invoke(third);

                Assert.AreSame(first, fakeAction.Invocations.ElementAt(0).Parameter);
                Assert.AreSame(second, fakeAction.Invocations.ElementAt(1).Parameter);
                Assert.AreSame(third, fakeAction.Invocations.ElementAt(2).Parameter);
            }

            [TestMethod]
            public void ShouldReturnValueFromFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeAction = new FakeFunc<object, object>(p => toReturn);

                var returned = fakeAction.Invoke(null);

                Assert.AreSame(toReturn, returned);
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

                var fakeAction = new FakeFunc<object, object, object>(func);

                fakeAction.Invoke(ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeFuncIsInvoked()
            {
                Func<object, object, object> func = (o1, o2) => null;

                object ignored = null;

                var fakeAction = new FakeFunc<object, object, object>(func);

                fakeAction.Invoke(ignored, ignored);
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored);
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored);
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToFuncWhenFakeFuncIsInvoked()
            {
                var p1 = new object();
                var p2 = new object();

                var fakeAction = new FakeFunc<object, object, object>((o1, o2) => null);

                fakeAction.Invoke(p1, p2);

                Assert.AreSame(p1, fakeAction.Invocations.First().FirstParameter);
                Assert.AreSame(p2, fakeAction.Invocations.First().SecondParameter);
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

                var fakeAction = new FakeFunc<object, object, object>((o1, o2) => null);

                fakeAction.Invoke(first1, first2);
                fakeAction.Invoke(second1, second2);
                fakeAction.Invoke(third1, third2);

                Assert.AreSame(first1, fakeAction.Invocations.ElementAt(0).FirstParameter);
                Assert.AreSame(first2, fakeAction.Invocations.ElementAt(0).SecondParameter);

                Assert.AreSame(second1, fakeAction.Invocations.ElementAt(1).FirstParameter);
                Assert.AreSame(second2, fakeAction.Invocations.ElementAt(1).SecondParameter);

                Assert.AreSame(third1, fakeAction.Invocations.ElementAt(2).FirstParameter);
                Assert.AreSame(third2, fakeAction.Invocations.ElementAt(2).SecondParameter);
            }

            [TestMethod]
            public void ShouldReturnValueFromFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeAction = new FakeFunc<object, object, object>((o1, o2) => toReturn);

                var returned = fakeAction.Invoke(null, null);

                Assert.AreSame(toReturn, returned);
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

                var fakeAction = new FakeFunc<object, object, object, object>(func);

                fakeAction.Invoke(ignored, ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeFuncIsInvoked()
            {
                Func<object, object, object, object> func = (o1, o2, o3) => null;

                object ignored = null;

                var fakeAction = new FakeFunc<object, object, object, object>(func);

                fakeAction.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToFuncWhenFakeFuncIsInvoked()
            {
                var p1 = new object();
                var p2 = new object();
                var p3 = new object();

                var fakeAction = new FakeFunc<object, object, object, object>((o1, o2, o3) => null);

                fakeAction.Invoke(p1, p2, p3);

                Assert.AreSame(p1, fakeAction.Invocations.First().FirstParameter);
                Assert.AreSame(p2, fakeAction.Invocations.First().SecondParameter);
                Assert.AreSame(p3, fakeAction.Invocations.First().ThirdParameter);
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

                var fakeAction = new FakeFunc<object, object, object, object>((o1, o2, o3) => null);

                fakeAction.Invoke(first1, first2, first3);
                fakeAction.Invoke(second1, second2, second3);
                fakeAction.Invoke(third1, third2, third3);

                Assert.AreSame(first1, fakeAction.Invocations.ElementAt(0).FirstParameter);
                Assert.AreSame(first2, fakeAction.Invocations.ElementAt(0).SecondParameter);
                Assert.AreSame(first3, fakeAction.Invocations.ElementAt(0).ThirdParameter);

                Assert.AreSame(second1, fakeAction.Invocations.ElementAt(1).FirstParameter);
                Assert.AreSame(second2, fakeAction.Invocations.ElementAt(1).SecondParameter);
                Assert.AreSame(second3, fakeAction.Invocations.ElementAt(1).ThirdParameter);

                Assert.AreSame(third1, fakeAction.Invocations.ElementAt(2).FirstParameter);
                Assert.AreSame(third2, fakeAction.Invocations.ElementAt(2).SecondParameter);
                Assert.AreSame(third3, fakeAction.Invocations.ElementAt(2).ThirdParameter);
            }

            [TestMethod]
            public void ShouldReturnValueFromFuncWhenInvoked()
            {
                var toReturn = new object();

                var fakeAction = new FakeFunc<object, object, object, object>((o1, o2, o3) => toReturn);

                var returned = fakeAction.Invoke(null, null, null);

                Assert.AreSame(toReturn, returned);
            }
        }
    }
}
