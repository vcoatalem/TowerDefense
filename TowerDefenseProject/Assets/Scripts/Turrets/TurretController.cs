using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TurretController : MonoBehaviour
{
    public enum TurretAction
    {
        NOTHING,
        SHOOT,
        ABILITY
    };

    protected int price;
    protected float range;
    protected Vector2 size;


    public virtual TurretAction Behave(Vector3 position) { return TurretAction.NOTHING; }

    public List<EnemyController> GetEnemiesInRange(Vector3 position)
    {
        Collider[] results = new Collider[128];
        int enemyLayerMask = 1 << 6;
        int enemyAmount = Physics.OverlapSphereNonAlloc(position, range, results, enemyLayerMask, QueryTriggerInteraction.Collide);
        //Debug.Log(enemyAmount);
        return results
            .Where(col => col != null)
            .Select(col => col.gameObject.GetComponent<EnemyController>())
            .ToList();
    }

    void Update()
    {
        Behave(transform.position); //TODO: should this be called every frame ? (prob not)
    }

}
