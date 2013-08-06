namespace FakeWin8
{
    using System;

    public class FakeAction<T1, T2, T3> : FakeMethodBase<T1, T2, T3>
    {
        private readonly Action<T1, T2, T3> action;

        public FakeAction(Action<T1, T2, T3> action)
        {
            this.action = action;
        }

        public void Invoke(T1 param1, T2 param2, T3 param3)
        {
            this.HandleInvocation(param1, param2, param3);

            this.action.Invoke(param1, param2, param3);
        }

        public FakeAction<T1, T2, T3> Accept(
            Func<T1, bool> param1Predicate,
            Func<T2, bool> param2Predicate,
            Func<T3, bool> param3Predicate)
        {
            this.AcceptInternal(param1Predicate, param2Predicate, param3Predicate);
            return this;
        }
    }
}
