using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret1 : TurretTemplate
{

    public EnemyController GetClosestEnemy(Vector3 position,  List<EnemyController> enemies)
    {
        if (enemies.Count == 0)
        {
            return null;
        }
        float minDistance = int.MaxValue;
        int index = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            float distance = Vector3.Distance(enemies[i].transform.position, position);
            if (distance < minDistance)
            {
                index = i;
                minDistance = distance;
            }
        }
        return enemies[index];
    }

    public override TurretAction Behave(Vector3 position)
    {

        List<EnemyController> enemies = GetEnemiesInRange(position);
        EnemyController closestEnemy = GetClosestEnemy(position, enemies);
        if (closestEnemy)
        {
            Debug.Log("closest ennemy: " + closestEnemy.GetInstanceID());
        }
        return TurretAction.NOTHING;
    }

    public Turret1() : base(10, 3f, new Vector2(1, 1), "Prefabs/Turret1") { }
}
