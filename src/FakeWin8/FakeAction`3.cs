namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeAction<T1, T2, T3> : FakeMethodBase, IFakeAction<T1, T2, T3>
    {
        private readonly Action<T1, T2, T3> action;

        private readonly IList<Invocation<T1, T2, T3>> invocations;

        public FakeAction(Action<T1, T2, T3> action)
        {
            this.action = action;
            this.invocations = new List<Invocation<T1, T2, T3>>();
        }

        public IEnumerable<Invocation<T1, T2, T3>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2, T3>>(this.invocations);
            }
        }

        public void Invoke(T1 param1, T2 param2, T3 param3)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2, param3));

            this.action.Invoke(param1, param2, param3);
        }
    }
}
