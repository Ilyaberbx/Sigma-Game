using System;
using Odumbrata.Core;
using Odumbrata.Extensions;
using UnityEngine.AI;

namespace Odumbrata.Components.Brains
{
    public abstract class BaseBrainSystem : BaseSystem
    {
        public NavMeshPath Path { get; protected set; }
        public event Action<NavMeshPath> OnPathValid;
        public event Action OnPathInvalid;

        protected void InformPathValid(NavMeshPath path)
        {
            OnPathValid.SafeInvoke(path);
        }

        protected void InformPathInvalid()
        {
            OnPathInvalid.SafeInvoke();
        }
    }
}