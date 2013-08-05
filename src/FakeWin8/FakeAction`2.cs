namespace FakeWin8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using FakeWin8.Conditions;

    public class FakeAction<T1, T2> : FakeMethodBase, IFakeAction<T1, T2>
    {
        private readonly Action<T1, T2> action;

        private readonly IList<Invocation<T1, T2>> invocations;

        private ParametersCondition<T1, T2> parametersCondition;

        public FakeAction(Action<T1, T2> action)
        {
            this.action = action;
            this.invocations = new List<Invocation<T1, T2>>();
        }

        public IEnumerable<Invocation<T1, T2>> Invocations
        {
            get
            {
                return new ReadOnlyCollection<Invocation<T1, T2>>(this.invocations);
            }
        }

        public void Invoke(T1 param1, T2 param2)
        {
            if (this.parametersCondition != null && !this.parametersCondition.IsMet(param1, param2))
            {
                throw new InvalidInvocationException(Resources.ParametersConditionNotMet);
            }

            this.HandleInvocation();

            this.invocations.Add(this.CreateInvocation(param1, param2));

            this.action.Invoke(param1, param2);
        }

        public FakeAction<T1, T2> AcceptOnly(Func<T1, bool> param1Condition, Func<T2, bool> param2Condition)
        {
            this.parametersCondition = new ParametersCondition<T1, T2>(param1Condition, param2Condition);
            return this;
        }
    }
}
