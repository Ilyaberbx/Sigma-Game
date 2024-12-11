using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Odumbrata.Core.Async
{
    public readonly struct AsyncOperationsGroup
    {
        private readonly List<AsyncOperation> _operations;

        public bool IsDone => _operations.All(temp => temp.isDone);
        public float Progress => _operations.Count == 0 ? 0 : _operations.Average(temp => temp.progress);

        public AsyncOperationsGroup(int initialCapacity)
        {
            _operations = new List<AsyncOperation>(initialCapacity);
        }

        public void Add(AsyncOperation operation)
        {
            _operations?.Add(operation);
        }

        public void Remove(AsyncOperation operation)
        {
            if (!_operations.Contains(operation))
            {
                return;
            }

            _operations?.Remove(operation);
        }
    }
}