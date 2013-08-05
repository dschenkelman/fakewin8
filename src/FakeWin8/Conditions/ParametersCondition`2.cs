namespace FakeWin8.Conditions
{
    using System;

    internal class ParametersCondition<T1, T2>
    {
        private readonly Func<T1, bool> parameter1Predicate;
        private readonly Func<T2, bool> parameter2Predicate;

        internal ParametersCondition(Func<T1, bool> parameter1Predicate, Func<T2, bool> parameter2Predicate)
        {
            this.parameter1Predicate = parameter1Predicate;
            this.parameter2Predicate = parameter2Predicate;
        }

        internal bool IsMet(T1 param1, T2 param2)
        {
            return this.parameter1Predicate.Invoke(param1) && this.parameter2Predicate.Invoke(param2);
        }
    }
}
