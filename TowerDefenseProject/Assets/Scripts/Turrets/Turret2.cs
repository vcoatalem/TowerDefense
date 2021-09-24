using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret2 : TurretController
{

    private Object turret2Ray;

    private EnemyController currentClosestEnemy = null;
    private Turret2RayController currentRay = null;

    private void Awake()
    {
        base.price = 10;
        base.range = 2f;
        base.size = new Vector2(1, 1);
        turret2Ray = Resources.Load("Prefabs/Turret2Ray");
    }
    public EnemyController GetClosestEnemy(Vector3 position,  List<EnemyController> enemies)
    {
        var closestEnemy = GetEnemiesInRange(position)
            .Select((enemy) => new { enemy, distance = Vector3.Distance(enemy.transform.position, position) })
            .OrderBy(enemyRange => -enemyRange.distance)
            .FirstOrDefault();
        if (closestEnemy != null)
        {
            return closestEnemy.enemy;
        }
        return null;
    }

    public override TurretAction Behave(Vector3 position)
    {
        List<EnemyController> enemies = GetEnemiesInRange(position);
        EnemyController closestEnemy = GetClosestEnemy(position, enemies);
        if (closestEnemy)
        {
            if (!currentRay)
            {
                currentRay = ((GameObject)Instantiate(turret2Ray, this.transform.position, Quaternion.identity, transform)).GetComponent<Turret2RayController>();
                currentRay.Initialize(this, closestEnemy);
            }

            if (closestEnemy != currentClosestEnemy)
            {
                if (currentClosestEnemy)
                {
                    currentRay.ChangeTarget(closestEnemy);
                }
                currentClosestEnemy = closestEnemy;
            }
              return TurretAction.SHOOT;
        }
        else
        { //this might not be needed
            if (currentRay)
            {
                Destroy(currentRay.gameObject);
            }
            return TurretAction.NOTHING;
        }
    }
}
