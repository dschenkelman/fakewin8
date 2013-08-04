namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeFunc<T1, T2, T3, TResult> : FakeMethodBase, IFakeFunc<T1, T2, T3, TResult>
    {
        private readonly Func<T1, T2, T3, TResult> function;

        private readonly IList<Invocation<T1, T2, T3>> invocations;

        public FakeFunc(Func<T1, T2, T3, TResult> function)
        {
            this.function = function;
            this.invocations = new List<Invocation<T1, T2, T3>>();
        }

        public IEnumerable<Invocation<T1, T2, T3>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2, T3>>(this.invocations);
            }
        }

        public TResult Invoke(T1 param1, T2 param2, T3 param3)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2, param3));

            return this.function.Invoke(param1, param2, param3);
        }
    }
}
