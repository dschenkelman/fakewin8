namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeWin8.Conditions;
    using FakeWin8.Exceptions;

    public abstract class FakeMethodBase<T1, T2> : FakeMethodBase
    {
        private readonly IList<Invocation<T1, T2>> invocations;

        private ParametersCondition<T1, T2> parametersCondition;

        protected FakeMethodBase()
        {
            this.invocations = new List<Invocation<T1, T2>>();
        }

        public IEnumerable<Invocation<T1, T2>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2>>(this.invocations);
            }
        }

        protected void AcceptOnlyInternal(Func<T1, bool> param1Predicate, Func<T2, bool> param2Predicate)
        {
            this.parametersCondition = new ParametersCondition<T1, T2>(param1Predicate, param2Predicate);
        }

        protected void HandleInvocation(T1 param1, T2 param2)
        {
            if (this.parametersCondition != null && !this.parametersCondition.IsMet(param1, param2))
            {
                throw new InvalidInvocationException(Resources.ParametersConditionNotMet);
            }

            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2));
        }

        protected Invocation<T1, T2> CreateInvocation(T1 param1, T2 param2)
        {
            return new Invocation<T1, T2> { FirstParameter = param1, SecondParameter = param2 };
        }
    }
}
