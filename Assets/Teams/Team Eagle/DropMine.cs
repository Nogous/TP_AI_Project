using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;

namespace Eagle
{
	[TaskCategory("AI_Eagle")]
	public class DropMine : Action
	{
		[Range(0.2f, 1f)]
		public SharedFloat MinEnergyNeeded = 1f;

		BehaviorTree _behaviorTree;

		public override void OnStart()
		{
			_behaviorTree = GetComponent<BehaviorTree>();

			GameData data = (_behaviorTree.GetVariable("GameData") as SharedGameData).Value;
			int _owner = (_behaviorTree.GetVariable("Owner") as SharedInt).Value;

			if (data.SpaceShips[_owner].Energy >= MinEnergyNeeded.Value)
			{
				data.SpaceShips[_owner].DropMine();

			}

		}

		public override TaskStatus OnUpdate()
		{
			
			return TaskStatus.Success;
		}
	}
}