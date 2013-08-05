namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeWin8.Conditions;
    using FakeWin8.Exceptions;
    using FakeWin8.Properties;

    public class FakeMethodBase<T1, T2, T3> : FakeMethodBase
    {
        private readonly IList<Invocation<T1, T2, T3>> invocations;

        private ParametersCondition<T1, T2, T3> parametersCondition;

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
            if (this.parametersCondition != null && !this.parametersCondition.IsMet(param1, param2, param3))
            {
                throw new InvalidInvocationException(Resources.ParametersConditionNotMet);
            }

            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2, param3));
        }

        protected void AcceptOnlyInternal(
            Func<T1, bool> param1Predicate,
            Func<T2, bool> param2Predicate,
            Func<T3, bool> param3Predicate)
        {
            this.parametersCondition = new ParametersCondition<T1, T2, T3>(param1Predicate, param2Predicate, param3Predicate);
        }

        protected Invocation<T1, T2, T3> CreateInvocation(T1 param1, T2 param2, T3 param3)
        {
            return new Invocation<T1, T2, T3> { FirstParameter = param1, SecondParameter = param2, ThirdParameter = param3 };
        }
    }
}