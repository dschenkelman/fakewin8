namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeFunc<T1, TResult> : FakeMethodBase, IFakeFunc<T1, TResult>
    {
        private readonly Func<T1, TResult> function;

        private readonly IList<Invocation<T1>> invocations;

        public FakeFunc(Func<T1, TResult> function)
        {
            this.function = function;
            this.invocations = new List<Invocation<T1>>();
        }

        public IEnumerable<Invocation<T1>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1>>(this.invocations);
            }
        }

        public TResult Invoke(T1 param1)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1));

            return this.function.Invoke(param1);
        }
    }
}
