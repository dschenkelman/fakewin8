namespace FakeWin8.Conditions
{
    using System;

    public class ParametersCondition<T1, T2, T3>
    {
        private readonly Func<T1, bool> param1Predicate;

        private readonly Func<T2, bool> param2Predicate;

        private readonly Func<T3, bool> param3Predicate;

        public ParametersCondition(
            Func<T1, bool> param1Predicate,
            Func<T2, bool> param2Predicate, 
            Func<T3, bool> param3Predicate)
        {
            this.param1Predicate = param1Predicate;
            this.param2Predicate = param2Predicate;
            this.param3Predicate = param3Predicate;
        }

        internal bool IsMet(T1 param1, T2 param2, T3 param3)
        {
            return this.param1Predicate.Invoke(param1)
                && this.param2Predicate.Invoke(param2)
                && this.param3Predicate.Invoke(param3);
        }
    }
}