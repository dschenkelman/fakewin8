namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeWin8.Conditions;

    public class FakeAction<T1> : FakeMethodBase, IFakeAction<T1>
    {
        private readonly Action<T1> action;

        private readonly IList<Invocation<T1>> invocations;

        private ParametersCondition<T1> parametersCondition;

        public FakeAction(Action<T1> action)
        {
            this.action = action;
            this.invocations = new List<Invocation<T1>>();
        }

        public IEnumerable<Invocation<T1>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1>>(this.invocations);
            }
        }

        public void Invoke(T1 param1)
        {
            if (this.parametersCondition != null && !this.parametersCondition.IsMet(param1))
            {
                throw new InvalidInvocationException(Resources.ParametersConditionNotMet);
            }

            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1));

            this.action.Invoke(param1);
        }

        public FakeAction<T1> AcceptOnly(Func<T1, bool> param1Condition)
        {
            this.parametersCondition = new ParametersCondition<T1>(param1Condition);
            return this;
        }
    }
}
