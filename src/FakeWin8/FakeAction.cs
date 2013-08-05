namespace FakeWin8
{
    using System;

    public class FakeAction : FakeMethodBase
    {
        private readonly Action action;

        public FakeAction(Action action)
        {
            this.action = action;
        }

        public void Invoke()
        {
            this.HandleInvocation();

            this.action.Invoke();
        }
    }
}
