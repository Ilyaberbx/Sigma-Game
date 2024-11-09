using System.Collections.Generic;
using System.Linq;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Utils;

namespace Odumbrata.Core
{
    public class Register<TElement> : IRegister<TElement>
    {
        public IReadOnlyList<TElement> Elements => _elements;

        private readonly List<TElement> _elements = new();

        public bool Contains(TElement element)
        {
            return Elements.Contains(element);
        }

        public void Add(TElement element)
        {
            if (ObjectHelper.IsNull(element))
            {
                return;
            }

            _elements.Add(element);
        }

        public void Remove(TElement element)
        {
            if (ObjectHelper.IsNull(element))
            {
                return;
            }

            if (_elements.IsNullOrEmpty())
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