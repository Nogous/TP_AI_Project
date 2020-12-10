using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace GoldSquadron
{
    public class Task_Shockwave : Action
    {
        public SharedGameObject behaviorGameObject;
        public float energyToSave = 0;

        GoldSquadronDefenseSystem defenseSystem;


        public override void OnAwake()
        {
            defenseSystem = GetDefaultGameObject(behaviorGameObject.Value).GetComponent<GoldSquadronDefenseSystem>();
        }

        public override TaskStatus OnUpdate()
        {
            defenseSystem.StartShockwave(energyToSave);
            return TaskStatus.Success;
        }
    }
}
