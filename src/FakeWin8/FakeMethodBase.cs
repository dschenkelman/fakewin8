namespace FakeWin8
{
    public abstract class FakeMethodBase
    {
        public int NumberOfInvocations { get; set; }

        protected void HandleInvocation()
        {
            this.NumberOfInvocations++;
        }

        protected Invocation<T1, T2, T3> CreateInvocation<T1, T2, T3>(T1 param1, T2 param2, T3 param3)
        {
            return new Invocation<T1, T2, T3>
                {
                    FirstParameter = param1, 
                    SecondParameter = param2, 
                    ThirdParameter = param3
                };
        }
    }
}