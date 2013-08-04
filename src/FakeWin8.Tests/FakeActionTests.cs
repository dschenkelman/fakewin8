namespace FakeWin8.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class FakeActionTests
    {
        [TestClass]
        public class NoParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action action = () => { actionCalled = true; };

                var fakeAction = FakeMethodFactory.CreateAction(action);

                fakeAction.Invoke();

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                var fakeAction = FakeMethodFactory.CreateAction(() => { });

                fakeAction.Invoke();
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke();
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke();
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }
        }

        [TestClass]
        public class OneParameter
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action<object> action = o => { actionCalled = true; };
                object ignored = null;

                var fakeAction = FakeMethodFactory.CreateAction(action);

                fakeAction.Invoke(ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                object ignored = null;
                var fakeAction = FakeMethodFactory.CreateAction<object>(o => { });

                fakeAction.Invoke(ignored);
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored);
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored);
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToActionWhenFakeActionIsInvoked()
            {
                object o = new object();

                var fakeAction = FakeMethodFactory.CreateAction<object>(p => { });

                fakeAction.Invoke(o);
                
                Assert.AreSame(o, fakeAction.Invocations.First().Parameter);
            }

            [TestMethod]
            public void ShouldKeepTrackOfParametersForConsecutiveInvocations()
            {
                var first = new object();
                var second = new object();
                var third = new object();

                var fakeAction = FakeMethodFactory.CreateAction<object>(p => { });

                fakeAction.Invoke(first);
                fakeAction.Invoke(second);
                fakeAction.Invoke(third);

                Assert.AreSame(first, fakeAction.Invocations.ElementAt(0).Parameter);
                Assert.AreSame(second, fakeAction.Invocations.ElementAt(1).Parameter);
                Assert.AreSame(third, fakeAction.Invocations.ElementAt(2).Parameter);
            }
        }

        [TestClass]
        public class TwoParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action<object, object> action = (o1, o2) => { actionCalled = true; };
                object ignored = null;

                var fakeAction = FakeMethodFactory.CreateAction(action);

                fakeAction.Invoke(ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                object ignored = null;
                var fakeAction = new FakeAction<object,object>((o1, o2) => { });

                fakeAction.Invoke(ignored, ignored);
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored);
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored);
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToActionWhenFakeActionIsInvoked()
            {
                object param1 = new object();
                object param2 = new object();

                var fakeAction = FakeMethodFactory.CreateAction<object, object>((o1, o2) => { });

                fakeAction.Invoke(param1, param2);

                Assert.AreSame(param1, fakeAction.Invocations.First().FirstParameter);
                Assert.AreSame(param2, fakeAction.Invocations.First().SecondParameter);
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

                var fakeAction = FakeMethodFactory.CreateAction<object, object>((o1, o2) => { });

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
        }

        [TestClass]
        public class ThreeParameters
        {
            [TestMethod]
            public void ShouldInvokeProvidedActionWhenFakeActionIsInvoked()
            {
                bool actionCalled = false;
                Action<object, object, object> action = (o1, o2, o3) => { actionCalled = true; };
                object ignored = null;

                var fakeAction = FakeMethodFactory.CreateAction(action);

                fakeAction.Invoke(ignored, ignored, ignored);

                Assert.IsTrue(actionCalled);
            }

            [TestMethod]
            public void ShouldIncreaseNumberOfInvocationsEachTimeFakeActionIsInvoked()
            {
                object ignored = null;
                var fakeAction = FakeMethodFactory.CreateAction<object, object, object>((o1, o2, o3) => { });

                fakeAction.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(1, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(2, fakeAction.NumberOfInvocations);

                fakeAction.Invoke(ignored, ignored, ignored);
                Assert.AreEqual(3, fakeAction.NumberOfInvocations);
            }

            [TestMethod]
            public void ShouldPassParameterToActionWhenFakeActionIsInvoked()
            {
                object param1 = new object();
                object param2 = new object();
                object param3 = new object();

                var fakeAction = FakeMethodFactory.CreateAction<object, object, object>((o1, o2, o3) => { });

                fakeAction.Invoke(param1, param2, param3);

                Assert.AreSame(param1, fakeAction.Invocations.First().FirstParameter);
                Assert.AreSame(param2, fakeAction.Invocations.First().SecondParameter);
                Assert.AreSame(param3, fakeAction.Invocations.First().ThirdParameter);
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

                var fakeAction = FakeMethodFactory.CreateAction<object, object, object>((o1, o2, o3) => { });

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
        }
    }
}