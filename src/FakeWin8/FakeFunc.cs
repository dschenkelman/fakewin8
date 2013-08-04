namespace FakeWin8
{
    using System;

    public class FakeFunc<TResult> : FakeMethodBase, IFakeFunc<TResult>
    {
        private readonly Func<TResult> function;

        public FakeFunc(Func<TResult> function)
        {
            this.function = function;
        }

        public TResult Invoke()
        {
            this.HandleInvocation();

            return this.function.Invoke();
        }
    }
}