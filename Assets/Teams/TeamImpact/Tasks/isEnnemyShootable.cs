using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class isEnnemyShootable : Conditional
	{
		public SharedVector3 currentPosition;
		public SharedFloat orientation;

		public SharedVector3 currentEnnemyPosition;
		public SharedVector2 ennemyVelocity;

		public float bulletSpeed = 5f;
		public float timeTolerance = 0.1f;
		public float maxDistanceToShoot = 10f;

		public override TaskStatus OnUpdate()
		{
			float shootAngle = Mathf.Deg2Rad * orientation.Value;
			Vector2 shootDir = new Vector2(Mathf.Cos(shootAngle), Mathf.Sin(shootAngle));


			Vector2 intersection;
			Vector2 curPos = new Vector2(currentPosition.Value.x, currentPosition.Value.y);
			Vector2 ennPos = new Vector2(currentEnnemyPosition.Value.x, currentEnnemyPosition.Value.y);
			bool canIntersect = MathUtils.ComputeIntersection(curPos, shootDir, ennPos, ennemyVelocity.Value, out intersection);

			float distanceBetweenShips = (curPos - ennPos).magnitude;

			if (!canIntersect || distanceBetweenShips > maxDistanceToShoot)
			{
				return TaskStatus.Failure;
			}

			Vector2 aiToI = intersection - curPos;
			Vector2 enemyToI = intersection - ennPos;
			if (Vector2.Dot(aiToI, shootDir) <= 0)
				return TaskStatus.Failure;

			float bulletTimeToI = aiToI.magnitude / bulletSpeed;
			float enemyTimeToI = enemyToI.magnitude / ennemyVelocity.Value.magnitude;
			enemyTimeToI *= Vector2.Dot(enemyToI, ennemyVelocity.Value) > 0 ? 1 : -1;

			float timeDiff = bulletTimeToI - enemyTimeToI;

			if (Mathf.Abs(timeDiff) < timeTolerance)
            {
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}
