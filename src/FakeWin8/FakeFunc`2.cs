namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeFunc<T1, T2, TResult> : FakeMethodBase, IFakeFunc<T1, T2, TResult>
    {
        private readonly Func<T1, T2, TResult> function;

        private readonly IList<Invocation<T1, T2>> invocations;

        public FakeFunc(Func<T1, T2, TResult> function)
        {
            this.function = function;
            this.invocations = new List<Invocation<T1, T2>>();
        }

        public IEnumerable<Invocation<T1, T2>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2>>(this.invocations);
            }
        }

        public TResult Invoke(T1 param1, T2 param2)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2));

            return this.function.Invoke(param1, param2);
        }
    }
}
