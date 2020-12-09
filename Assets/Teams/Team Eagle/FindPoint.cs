using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using DoNotModify;
using UnityEngine;

namespace Eagle
{
    [TaskCategory("AI_Eagle")]
    public class FindPoint : Action
    {
        BehaviorTree _behaviorTree;

        public override void OnStart()
        {
            _behaviorTree = GetComponent<BehaviorTree>();
        }

        public override TaskStatus OnUpdate()
        {

            _behaviorTree.SetVariableValue("State", 1);
            /*
            float tmpDist = Vector2.Distance(data.WayPoints[0].Position, AI_Eagle.myShip.Position);
            Vector2 nextPos = data.WayPoints[0].Position;

            for (int i = 1; i < data.WayPoints.Count; i++)
            {
                if (tmpDist > Vector2.Distance(data.WayPoints[i].Position, AI_Eagle.myShip.Position))
                {
                    nextPos = data.WayPoints[i].Position;
                }
            }

            Debug.Log(nextPos);
            */
            return TaskStatus.Success;
        }
    }
}