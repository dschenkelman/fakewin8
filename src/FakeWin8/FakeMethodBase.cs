namespace FakeWin8
{
    public abstract class FakeMethodBase : IFakeMethodBase
    {
        public int NumberOfInvocations { get; set; }

        internal void HandleInvocation()
        {
            this.NumberOfInvocations++;
        }

        internal Invocation<T1> CreateInvocation<T1>(T1 param1)
        {
            return new Invocation<T1> { Parameter = param1 };
        }

        internal Invocation<T1, T2> CreateInvocation<T1, T2>(T1 param1, T2 param2)
        {
            return new Invocation<T1, T2> { FirstParameter = param1, SecondParameter = param2 };
        }

        internal Invocation<T1, T2, T3> CreateInvocation<T1, T2, T3>(T1 param1, T2 param2, T3 param3)
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