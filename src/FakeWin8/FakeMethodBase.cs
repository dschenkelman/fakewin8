namespace FakeWin8
{
    public abstract class FakeMethodBase
    {
        public int NumberOfInvocations { get; set; }

        protected void HandleInvocation()
        {
            this.NumberOfInvocations++;
        }
    }
}