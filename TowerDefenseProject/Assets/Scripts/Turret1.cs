using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret1 : TurretController
{
    // cooldown ?
    private float cooldown = 0.2f;
    private bool isOnCooldown = false;

    private Object bullet;

    IEnumerator Cooldown()
    {
        //Debug.Log("start cooldown subroutine");
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
        //Debug.Log("end cooldown subroutine");
    }

    private void Awake()
    {
        base.price = 10;
        base.range = 2f;
        base.size = new Vector2(1, 1);
        bullet = Resources.Load("Prefabs/Turret1Bullet");
    }
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
        if (closestEnemy && !isOnCooldown)
        {
            StartCoroutine("Cooldown");
            GameObject instantiated = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity, transform);
            Turret1Bullet instantiatedBullet = instantiated.GetComponent<Turret1Bullet>();
            instantiatedBullet.Initialize(20f, 10, 5f, closestEnemy);
            return TurretAction.SHOOT;
        }
        return TurretAction.NOTHING;
    }
}
