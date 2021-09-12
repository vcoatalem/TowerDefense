using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TurretTemplate
{

    public enum TurretAction
    {
        NOTHING,
        SHOOT,
        ABILITY
    };

    public int price { get; }
    public float range { get; }
    public Vector2 size { get; }
    public Object model { get; }
    // cooldown ?

    public TurretTemplate(int price, float range, Vector2 size, string filepath)
    {
        this.price = price;
        this.range = range;
        this.size = size;
        model = Resources.Load(filepath);
    }

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

}
