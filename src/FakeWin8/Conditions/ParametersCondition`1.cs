namespace FakeWin8.Conditions
{
    using System;

    internal class ParametersCondition<T>
    {
        private readonly Func<T, bool> parameterPredicate;

        internal ParametersCondition(Func<T, bool> parameterPredicate)
        {
            this.parameterPredicate = parameterPredicate;
        }

        internal bool IsMet(T parameter)
        {
            return this.parameterPredicate.Invoke(parameter);
        }
    }
}
