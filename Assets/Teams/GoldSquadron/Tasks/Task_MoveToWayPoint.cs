using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace GoldSquadron
{
    public class Task_MoveToWayPoint : Action
    {
        public SharedGameObject behaviorGameObject;

        GoldSquadronMovementSystem movementSystem;

        public override void OnAwake()
        {
            movementSystem = GetDefaultGameObject(behaviorGameObject.Value).GetComponent<GoldSquadronMovementSystem>();
        }

        public override TaskStatus OnUpdate()
        {
            if (movementSystem.MoveToPoint() == true)
                return TaskStatus.Success;
            return TaskStatus.Running;
        }
    }
}
