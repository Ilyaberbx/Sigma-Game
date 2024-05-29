using System.Collections.Generic;

namespace Odumbrata.Commons.DataManagement
{
    public interface IElementsHolder<out TCollection, TElement> where TCollection : IReadOnlyCollection<TElement>
    {
        public TCollection Elements { get; }
    }
}