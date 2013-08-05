namespace FakeWin8
{
    using System;

    using FakeWin8.Conditions;
    using FakeWin8.Exceptions;
    using FakeWin8.Properties;

    public class FakeFunc<T1, T2, T3, TResult> : FakeMethodBase<T1, T2, T3>
    {
        private readonly Func<T1, T2, T3, TResult> function;

        private ParametersCondition<T1, T2, T3> parametersCondition;

        public FakeFunc(Func<T1, T2, T3, TResult> function)
        {
            this.function = function;
        }

        public TResult Invoke(T1 param1, T2 param2, T3 param3)
        {
            this.HandleInvocation(param1, param2, param3);

            return this.function.Invoke(param1, param2, param3);
        }

        public FakeFunc<T1, T2, T3, TResult> AcceptOnly(Func<T1, bool> param1Predicate, Func<T2, bool> param2Predicate, Func<T3, bool> param3Predicate)
        {
            this.AcceptOnlyInternal(param1Predicate, param2Predicate, param3Predicate);
            return this;
        }
    }
}
