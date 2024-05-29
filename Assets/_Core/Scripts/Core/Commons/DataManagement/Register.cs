using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;

namespace Odumbrata.Commons.DataManagement
{
    public class Register<TElement> : ISubscriptionHandler<TElement>, IElementsHolder<IReadOnlyList<TElement>, TElement>
    {
        public IReadOnlyList<TElement> Elements => _elements;

        private readonly List<TElement> _elements;

        public Register()
        {
            _elements = new List<TElement>();
        }

        public void Subscribe(TElement element)
        {
            if (element != null)
            {
                _elements.Add(element);
            }
        }

        public void Unsubscribe(TElement element)
        {
            if (element == null)
            {
                return;
            }

            if (_elements.IsEmpty())
            {
                return;
            }

            if (_elements.Contains(element))
            {
                _elements.Remove(element);
            }
        }
    }
}