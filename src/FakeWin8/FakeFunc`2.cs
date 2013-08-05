namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeWin8.Conditions;

    public class FakeFunc<T1, T2, TResult> : FakeMethodBase
    {
        private readonly Func<T1, T2, TResult> function;

        private readonly IList<Invocation<T1, T2>> invocations;

        private ParametersCondition<T1, T2> parametersCondition;

        public FakeFunc(Func<T1, T2, TResult> function)
        {
            this.function = function;
            this.invocations = new List<Invocation<T1, T2>>();
        }

        public IEnumerable<Invocation<T1, T2>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2>>(this.invocations);
            }
        }

        public TResult Invoke(T1 param1, T2 param2)
        {
            if (this.parametersCondition != null && !this.parametersCondition.IsMet(param1, param2))
            {
                throw new InvalidInvocationException(Resources.ParametersConditionNotMet);
            }

            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2));

            return this.function.Invoke(param1, param2);
        }

        public FakeFunc<T1, T2, TResult> AcceptOnly(Func<T1, bool> param1Predicate, Func<T2, bool> param2Predicate)
        {
            this.parametersCondition = new ParametersCondition<T1, T2>(param1Predicate, param2Predicate);

            return this;
        }
    }
}