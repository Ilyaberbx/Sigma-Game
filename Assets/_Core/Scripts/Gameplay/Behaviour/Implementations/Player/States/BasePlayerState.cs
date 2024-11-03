using System;

namespace Odumbrata.Behaviour.Player.States
{
    [Serializable]
    public abstract class BasePlayerState : BaseBehaviourState
    {
    }

    [Serializable]
    public abstract class BasePlayerState<TData> : BaseBehaviourState<TData>
    {
    }
}