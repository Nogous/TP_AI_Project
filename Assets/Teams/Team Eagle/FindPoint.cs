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

        Vector2 focusPosition;

        public override void OnStart()
        {
            _behaviorTree = GetComponent<BehaviorTree>();
        }

        public override TaskStatus OnUpdate()
        {
            GameData data = (_behaviorTree.GetVariable("GameData") as SharedGameData).Value;
            int _owner = (_behaviorTree.GetVariable("Owner") as SharedInt).Value;


            float tmpDist = Vector2.Distance(data.WayPoints[0].Position, data.SpaceShips[_owner].Position);
            int index = 0;

            for (int i = 0; i < data.WayPoints.Count; i++)
            {
                if (tmpDist > Vector2.Distance(data.WayPoints[i].Position, data.SpaceShips[_owner].Position))
                {
                    tmpDist = Vector2.Distance(data.WayPoints[i].Position, data.SpaceShips[_owner].Position);
                    index = i;
                }
            }

            _behaviorTree.SetVariableValue("targetPosition", data.WayPoints[index].Position);
            focusPosition = data.WayPoints[index].Position;

            return TaskStatus.Success;
        }
    }
}