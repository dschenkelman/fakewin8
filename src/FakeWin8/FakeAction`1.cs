namespace FakeWin8
{
    using System;

    using FakeWin8.Conditions;

    public class FakeAction<T1> : FakeMethodBase<T1>
    {
        private readonly Action<T1> action;

        public FakeAction(Action<T1> action)
        {
            this.action = action;
        }

        public void Invoke(T1 param1)
        {
            this.HandleInvocation(param1);

            this.action.Invoke(param1);
        }

        public FakeAction<T1> Accept(Func<T1, bool> param1Condition)
        {
            this.AcceptInternal(param1Condition);
            return this;
        }
    }
}