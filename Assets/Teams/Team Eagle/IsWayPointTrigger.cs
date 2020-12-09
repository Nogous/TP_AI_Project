using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DoNotModify;

namespace Eagle
{
	public class IsWayPointTrigger : Action
	{
		BehaviorTree _behaviorTree;

		WayPoint wayPointA;

		public override void OnStart()
		{
			_behaviorTree = GetComponent<BehaviorTree>();

			wayPointA = (_behaviorTree.GetVariable("Owner") as SharedWayPoint).Value;
		}

		public override TaskStatus OnUpdate()
		{

			Debug.Log(wayPointA.Owner +" : "+ (_behaviorTree.GetVariable("Owner") as SharedInt).Value);
            if (wayPointA.Owner == (_behaviorTree.GetVariable("Owner") as SharedInt).Value)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}
	}
}