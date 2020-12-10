using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;

namespace GoldSquadron
{
    public class GoldSquadronDefenseSystem : MonoBehaviour
    {
        [SerializeField]
        float distanceForShockwave = 6.0f;
        [SerializeField]
        float cooldownShockwaveTime = 1f;

        [Header("Mine Defense")]
        [SerializeField]
        float mineDefendingRadius = 5f;

        private bool shockwave;
        public bool Shockwave
        {
            get { return shockwave; }
        }

        private bool mineInSight;
        public bool MineInSight
        {
            get { return mineInSight; }
        }



        // Faire un script parent pour éviter le C-v C-c
        GameData data;

        SpaceShip ship;
        SpaceShip enemyShip;
        float cooldownShockwave = 0f;


        public void InitializeSystem(SpaceShip spaceship, SpaceShip enemySpaceship, GameData newData)
        {
            data = newData;
            ship = spaceship;
            enemyShip = enemySpaceship;
        }

        public void UpdateSystem(GameData newData)
        {
            // Y'a besoin de faire ça ? On a pas une référence normalement ?
            data = newData;
            DefenseMineLongRange();
        }

        public void LateUpdateSystem(GameData newData)
        {
            if (cooldownShockwave > 0) cooldownShockwave -= Time.deltaTime;
            if (shockwave == true)
            {
                shockwave = false;
                cooldownShockwave = cooldownShockwaveTime;
            }
        }




    private void DefenseMineLongRange()
        {
            int layerMask = 1 << 16;
            layerMask |= 1 << 12;
            RaycastHit2D hit = Physics2D.Raycast(ship.transform.position, ship.transform.right, 10f, layerMask);
            Debug.DrawRay(ship.transform.position, ship.transform.right * 10f, Color.red, 0.1f);
            if (hit)
            {
                Mine mine = hit.transform.GetComponentInParent<Mine>(); // Faut gagner hein
                if(mine != null)
                {
                    if (mine.IsActive)
                    {
                        if (DetectIfMineEnemy(mine.transform.position))
                        {
                            mineInSight = true;
                            return;
                        }
                    }
                }
            }
            mineInSight = false;
        }

        private bool DetectIfMineEnemy(Vector3 minePosition)
        {
            Vector3 direction;
            direction = minePosition - ship.transform.position;
            if (direction.sqrMagnitude < mineDefendingRadius) // La mine est vraiment proche, donc on est en danger
            {
                return true;
            }

            for (int i = 0; i < data.WayPoints.Count; i++) // Sinon on regarde si il y a un waypoint à côté, si il y en a un et qu'il est de notre couleur on ne tire pas
            {
                direction = data.WayPoints[i].transform.position - minePosition;
                if(direction.sqrMagnitude < mineDefendingRadius)
                {
                    if (data.WayPoints[i].Owner != ship.Owner)
                        return true;
                    return false;
                }               
            }
            return true;
        }


        public bool ShockwavePlayerDetector()
        {
            if (enemyShip.IsStun())
                return false;
            if (cooldownShockwave > 0)
            {
                cooldownShockwave -= Time.deltaTime;
                return false;
            }

            float distance = (enemyShip.transform.position - ship.transform.position).sqrMagnitude;
            if (distance < distanceForShockwave && ship.Energy > ship.ShockwaveEnergyCost)
            {
                cooldownShockwave = cooldownShockwaveTime;
                return true;
            }
            return false;
        }

        private void ShockwaveMineDetector()
        {
            /*Vector3 direction;
            for (int i = 0; i < data.Mines.Count; i++)
            {
                if (!data.Mines[i].IsActive)
                    continue;
                direction = data.Mines[i].transform.position - ship.transform.position;
                if(direction.sqrMagnitude > distanceForShockwave)
                {

                }


                RaycastHit2D hit = Physics2D.Raycast(start, target - start, (target - start).magnitude, layer);


                ship.Velocity
                Vector3 direction = data.Mines[i].transform.position - ship.transform.position;
                data.Mines[i].
            }*/
        }

        private void ShockwaveBulletDetecter()
        {
            Vector3 direction;
            for (int i = 0; i < data.Bullets.Count; i++)
            {
                direction = data.Bullets[i].transform.position - ship.transform.position;
                if(direction.sqrMagnitude > distanceForShockwave * 0.5f)
                {
                    if(Vector3.Dot(data.Bullets[i].Velocity, direction) > 0.9f)
                    {
                        // Shockwave
                        return;
                    }
                }
            }
        }

        public void StartShockwave(float energyToSave)
        {
            if (cooldownShockwave > 0) return;
            if (ship.Energy >= (energyToSave))
                shockwave = true;
        }

    }
}
