using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DoNotModify;

namespace Eagle {

	public class AI_Eagle : BaseSpaceShipController
	{
		private Blackboard _blackboard;

		private BehaviorTree _behaviorTree;

		public override void Initialize(SpaceShip spaceship, GameData data)
		{
			_behaviorTree = GetComponent<BehaviorTree>();

			_blackboard = GetComponent<Blackboard>();
			if (!_blackboard)
            {
				_blackboard = gameObject.AddComponent(typeof(Blackboard)) as Blackboard;
			}

			_blackboard.Initialize(spaceship, data);
		}

		public override InputData UpdateInput(SpaceShip spaceship, GameData data)
		{
			float thrust = (_behaviorTree.GetVariable("thrust") as SharedFloat).Value;
			float targetOrient = (_behaviorTree.GetVariable("targetOrient") as SharedFloat).Value;

			bool IsDropping = (_behaviorTree.GetVariable("IsDroppingMine") as SharedBool).Value;
			_behaviorTree.SetVariableValue("IsDroppingMine", false);

			_blackboard.UpdateData(data);

			return new InputData(thrust, targetOrient, _blackboard.TriggerShoot, IsDropping, false);
		}
	}
}
