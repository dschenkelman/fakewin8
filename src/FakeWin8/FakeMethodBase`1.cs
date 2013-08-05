namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeWin8.Conditions;

    public abstract class FakeMethodBase<T1> : FakeMethodBase
    {
        private readonly IList<Invocation<T1>> invocations;

        private ParametersCondition<T1> ParametersCondition { get; set; }

        protected FakeMethodBase()
        {
            this.invocations = new List<Invocation<T1>>();
        }

        public IEnumerable<Invocation<T1>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1>>(this.invocations);
            }
        }

        protected void AcceptOnlyInternal(Func<T1, bool> param1Condition)
        {
            this.ParametersCondition = new ParametersCondition<T1>(param1Condition);
        }

        protected void HandleInvocation(T1 param1)
        {
            if (this.ParametersCondition != null && !this.ParametersCondition.IsMet(param1))
            {
                throw new InvalidInvocationException(Resources.ParametersConditionNotMet);
            }

            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1));
        }

        protected Invocation<T1> CreateInvocation<T1>(T1 param1)
        {
            return new Invocation<T1> { Parameter = param1 };
        }
    }
}
