using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2RayController : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer line;

    private Turret2 source;
    private EnemyController target;

    private float timeBetweenHits = 0.2f;


    IEnumerator Hit()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenHits);
            target.TakeDamage(1);
        }
    }

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void ChangeTarget(EnemyController other)
    {
        target = other;
    }

    public void Initialize(Turret2 turret, EnemyController enemy)
    {
        source = turret;
        target = enemy;
        StartCoroutine("Hit");
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
        else
        {
            line.SetPositions(new Vector3[]
            {
            source.transform.position,
            target.transform.position
            });
        }
    }
}
