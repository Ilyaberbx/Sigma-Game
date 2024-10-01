using Odumbrata.Core.Conditions;

namespace Odumbrata.Utils
{
    public static class ConditionsHelper
    {
        public static bool Satisfy<TCondition>() where TCondition : ICondition, new()
        {
            var condition = new TCondition();

            condition.Initialize();

            return condition.Satisfy();
        }
    }
}