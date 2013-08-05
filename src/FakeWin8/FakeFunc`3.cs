namespace FakeWin8
{
    using System;

    public class FakeFunc<T1, T2, T3, TResult> : FakeMethodBase<T1, T2, T3>
    {
        private readonly Func<T1, T2, T3, TResult> function;

        public FakeFunc(Func<T1, T2, T3, TResult> function)
        {
            this.function = function;
        }

        public TResult Invoke(T1 param1, T2 param2, T3 param3)
        {
            this.HandleInvocation(param1, param2, param3);

            return this.function.Invoke(param1, param2, param3);
        }
    }
}
