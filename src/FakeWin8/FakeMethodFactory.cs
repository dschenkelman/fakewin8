namespace FakeWin8
{
    using System;

    public static class FakeMethodFactory
    {
        public static FakeAction CreateAction(Action action)
        {
            return new FakeAction(action);
        }

        public static FakeAction<T1> CreateAction<T1>(Action<T1> action)
        {
            return new FakeAction<T1>(action);
        }

        public static FakeAction<T1, T2> CreateAction<T1, T2>(Action<T1, T2> action)
        {
            return new FakeAction<T1, T2>(action);
        }

        public static FakeAction<T1, T2, T3> CreateAction<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            return new FakeAction<T1, T2, T3>(action);
        }

        public static FakeFunc<TResult> CreateFunc<TResult>(Func<TResult> function)
        {
            return new FakeFunc<TResult>(function);
        }

        public static FakeFunc<T1, TResult> CreateFunc<T1, TResult>(Func<T1, TResult> function)
        {
            return new FakeFunc<T1, TResult>(function);
        }

        public static FakeFunc<T1, T2, TResult> CreateFunc<T1, T2, TResult>(Func<T1, T2, TResult> function)
        {
            return new FakeFunc<T1, T2, TResult>(function);
        }

        public static FakeFunc<T1, T2, T3, TResult> CreateFunc<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function)
        {
            return new FakeFunc<T1, T2, T3, TResult>(function);
        }
    }
}
