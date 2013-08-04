namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeAction<T1> : FakeMethodBase, IFakeAction<T1>
    {
        private readonly Action<T1> action;

        private readonly IList<Invocation<T1>> invocations;

        public FakeAction(Action<T1> action)
        {
            this.action = action;
            this.invocations = new List<Invocation<T1>>();
        }

        public IEnumerable<Invocation<T1>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1>>(this.invocations);
            }
        }

        public void Invoke(T1 param1)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1));

            this.action.Invoke(param1);
        }
    }
}
