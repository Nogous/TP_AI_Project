using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace GoldSquadron
{
    public class Task_Shoot : Action
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
            UnityEngine.Debug.Log("Je shoot");
            weaponSystem.FireShoot(energyToSave);
            return TaskStatus.Success;
        }
    }
}
