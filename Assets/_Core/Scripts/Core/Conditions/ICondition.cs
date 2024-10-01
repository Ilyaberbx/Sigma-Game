using System;

namespace Odumbrata.Core.Conditions
{
    public interface ICondition : IDisposable
    {
        public void Initialize();

        public bool Satisfy();
    }
}