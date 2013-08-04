namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeAction<T1, T2> : FakeMethodBase, IFakeAction<T1, T2>
    {
        private readonly Action<T1, T2> action;

        private readonly IList<Invocation<T1, T2>> invocations;

        public FakeAction(Action<T1, T2> action)
        {
            this.action = action;
            this.invocations = new List<Invocation<T1, T2>>();
        }

        public IEnumerable<Invocation<T1, T2>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2>>(this.invocations);
            }
        }

        public void Invoke(T1 param1, T2 param2)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2));

            this.action.Invoke(param1, param2);
        }
    }
}
