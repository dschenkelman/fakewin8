namespace FakeWin8
{
    using System;

    public class FakeAction<T1, T2> : FakeMethodBase<T1, T2>
    {
        private readonly Action<T1, T2> action;

        public FakeAction(Action<T1, T2> action)
        {
            this.action = action;
        }

        public void Invoke(T1 param1, T2 param2)
        {
            this.HandleInvocation(param1, param2);

            this.action.Invoke(param1, param2);
        }

        public FakeAction<T1, T2> AcceptOnly(Func<T1, bool> param1Predicate, Func<T2, bool> param2Predicate)
        {
            this.AcceptOnlyInternal(param1Predicate, param2Predicate);
            return this;
        }
    }
}
