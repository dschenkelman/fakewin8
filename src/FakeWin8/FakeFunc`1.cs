namespace FakeWin8
{
    using System;

    public sealed class FakeFunc<T1, TResult> : FakeMethodBase<T1>
    {
        private readonly Func<T1, TResult> function;

        public FakeFunc(Func<T1, TResult> function)
        {
            this.function = function;
        }

        public TResult Invoke(T1 param1)
        {
            this.HandleInvocation(param1);

            return this.function.Invoke(param1);
        }

        public FakeFunc<T1, TResult> AcceptOnly(Func<T1, bool> param1Condition)
        {
            this.AcceptOnlyInternal(param1Condition);
            return this;
        }
    }
}
