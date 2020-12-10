using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime;

namespace GoldSquadron {

	public class GoldLeaderController : BaseSpaceShipController
	{
		SpaceShip enemySpaceship;

		//
		[SerializeField]
		public GoldSquadronMovementSystem movementSystem;

		[SerializeField]
		public GoldSquadronWeaponSystem weaponSystem;
		[SerializeField]
		public GoldSquadronDefenseSystem defenseSystem;
		[SerializeField]
		public GoldSquadronEnemyScan enemyScanSystem;

		BehaviorTree behaviorTree = null;

		public override void Initialize(SpaceShip spaceship, GameData data)
		{
			enemySpaceship = data.GetSpaceShipForOwner(0);
			if(enemySpaceship == spaceship)
				enemySpaceship = data.GetSpaceShipForOwner(1);

			//movementSystem = new GoldSquadronWeaponSystem(spaceship, data);

			if (movementSystem == null)
				movementSystem = GetComponent<GoldSquadronMovementSystem>();
			movementSystem.InitializeSystem(spaceship, data);

			if (weaponSystem == null)
				weaponSystem = GetComponent<GoldSquadronWeaponSystem>();
			weaponSystem.InitializeSystem(spaceship, enemySpaceship, data);

			if (defenseSystem == null)
				defenseSystem = GetComponent<GoldSquadronDefenseSystem>();
			defenseSystem.InitializeSystem(spaceship, enemySpaceship, data);

			behaviorTree = GetComponent<BehaviorTree>();
		}



		public override InputData UpdateInput(SpaceShip spaceship, GameData data)
		{
			movementSystem.UpdateSystem(data); 
			weaponSystem.UpdateSystem(data);
			defenseSystem.UpdateSystem(data);

			behaviorTree.SetVariableValue("EnemyInSight", weaponSystem.EnemyInSight);
			behaviorTree.SetVariableValue("MineInSight", defenseSystem.MineInSight);

			behaviorTree.SetVariableValue("IsStun", spaceship.IsStun());
			behaviorTree.SetVariableValue("IsHit", spaceship.IsHit());

			behaviorTree.SetVariableValue("EnemyIsStun", enemySpaceship.IsStun());
			behaviorTree.SetVariableValue("EnemyIsHit", enemySpaceship.IsHit());

			behaviorTree.SetVariableValue("DistanceToEnemy", (spaceship.transform.position - enemySpaceship.transform.position).sqrMagnitude);

			if ((bool) behaviorTree.GetVariable("MineInSight").GetValue() == true)
				weaponSystem.FireShoot(0.5f);
			if ((bool)behaviorTree.GetVariable("EnemyInSight").GetValue() == true)
			{
				weaponSystem.FireShoot(0.5f);
			}

			InputData input = new InputData(movementSystem.Thrust, movementSystem.Orient, weaponSystem.Shoot, weaponSystem.Mine, defenseSystem.Shockwave);

			weaponSystem.LateUpdateSystem(data);
			defenseSystem.LateUpdateSystem(data);
			return input;
		}

	}


}
