using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace GoldSquadron
{
    public class Task_LandMine : Action
    {
        public SharedGameObject behaviorGameObject;
        public float energyToSave = 0;

        GoldSquadronWeaponSystem weaponSystem;


        public override void OnAwake()
        {
            weaponSystem = GetDefaultGameObject(behaviorGameObject.Value).GetComponent<GoldSquadronWeaponSystem>();
        }

        public override TaskStatus OnUpdate()
        {
            weaponSystem.LandMine(energyToSave);
            return TaskStatus.Success;
        }
    }
}
