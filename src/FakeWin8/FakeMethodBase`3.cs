namespace FakeWin8
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FakeMethodBase<T1, T2, T3> : FakeMethodBase
    {
        private readonly IList<Invocation<T1, T2, T3>> invocations;

        protected FakeMethodBase()
        {
            this.invocations = new List<Invocation<T1, T2, T3>>();
        }

        public IEnumerable<Invocation<T1, T2, T3>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2, T3>>(this.invocations);
            }
        }

        protected void HandleInvocation(T1 param1, T2 param2, T3 param3)
        {
            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2, param3));
        }

        protected Invocation<T1, T2, T3> CreateInvocation(T1 param1, T2 param2, T3 param3)
        {
            return new Invocation<T1, T2, T3> { FirstParameter = param1, SecondParameter = param2, ThirdParameter = param3 };
        }
    }
}