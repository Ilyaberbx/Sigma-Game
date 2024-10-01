using System.Collections.Generic;
using Odumbrata.Systems;
using Odumbrata.Systems.Movement.States;

namespace Odumbrata.Core
{
    public interface IRegister<TElement>
    {
        public void Add(TElement element);
        public void Remove(TElement element);
        IReadOnlyList<TElement> Elements { get; }
    }
}