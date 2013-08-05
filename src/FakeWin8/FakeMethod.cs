namespace FakeWin8
{
    using System;

    public static class FakeMethod
    {
        public static FakeAction CreateFor(Action action)
        {
            return new FakeAction(action);
        }

        public static FakeAction<T1> CreateFor<T1>(Action<T1> action)
        {
            return new FakeAction<T1>(action);
        }

        public static FakeAction<T1, T2> CreateFor<T1, T2>(Action<T1, T2> action)
        {
            return new FakeAction<T1, T2>(action);
        }

        public static FakeAction<T1, T2, T3> CreateFor<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            return new FakeAction<T1, T2, T3>(action);
        }

        public static FakeFunc<TResult> CreateFor<TResult>(Func<TResult> function)
        {
            return new FakeFunc<TResult>(function);
        }

        public static FakeFunc<T1, TResult> CreateFor<T1, TResult>(Func<T1, TResult> function)
        {
            return new FakeFunc<T1, TResult>(function);
        }

        public static FakeFunc<T1, T2, TResult> CreateFor<T1, T2, TResult>(Func<T1, T2, TResult> function)
        {
            return new FakeFunc<T1, T2, TResult>(function);
        }

        public static FakeFunc<T1, T2, T3, TResult> CreateFor<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function)
        {
            return new FakeFunc<T1, T2, T3, TResult>(function);
        }
    }
}
