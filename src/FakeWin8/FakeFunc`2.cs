namespace FakeWin8
{
    using System;

    public class FakeFunc<T1, T2, TResult> : FakeMethodBase<T1, T2>
    {
        private readonly Func<T1, T2, TResult> function;

        public FakeFunc(Func<T1, T2, TResult> function)
        {
            this.function = function;
        }

        public TResult Invoke(T1 param1, T2 param2)
        {
            this.HandleInvocation(param1, param2);

            return this.function.Invoke(param1, param2);
        }

        public FakeFunc<T1, T2, TResult> Accept(Func<T1, bool> param1Predicate, Func<T2, bool> param2Predicate)
        {
            this.AcceptInternal(param1Predicate, param2Predicate);

            return this;
        }
    }
}