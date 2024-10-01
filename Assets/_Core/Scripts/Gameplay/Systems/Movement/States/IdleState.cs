using System;
using UnityEngine;

namespace Odumbrata.Systems.Movement.States
{
    [Serializable]
    public class IdleState : BaseMoveState
    {
        public override void OnEntered()
        {
            Debug.Log("Idle entered");
        }

        public override void OnExited()
        {
            Debug.Log("Idle exited");
        }
    }
}