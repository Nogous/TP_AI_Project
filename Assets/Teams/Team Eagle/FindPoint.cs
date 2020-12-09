using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using DoNotModify;
using UnityEngine;

namespace Eagle
{
    [TaskCategory("AI_Eagle")]
    public class FindPoint : Action
    {
        BehaviorTree _behaviorTree = null;
        Animator _stateMachine = null;

        int _owner;
        GameData _gameData = null;

        public override void OnStart()
        {
        }

        public override TaskStatus OnUpdate()
        {
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