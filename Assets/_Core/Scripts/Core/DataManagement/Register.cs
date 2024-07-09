using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Utils;

namespace Odumbrata.Core
{
    public class Register<TElement> : ISubscriptionHandler<TElement>
    {
        public IReadOnlyList<TElement> Elements => _elements;

        private readonly List<TElement> _elements;

        public Register()
        {
            _elements = new List<TElement>();
        }

        public void Subscribe(TElement element)
        {
            if (ObjectValidator.IsNull(element))
            {
                return;
            }

            _elements.Add(element);
        }

        public void Unsubscribe(TElement element)
        {
            if (ObjectValidator.IsNull(element))
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