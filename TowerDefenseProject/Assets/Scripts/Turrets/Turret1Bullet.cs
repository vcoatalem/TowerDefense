using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1Bullet : MonoBehaviour
{
    private float speed;
    private int damage;
    private float force;
    private EnemyController enemy;

    public void Initialize(float speed, int damage, float force, EnemyController enemy)
    {
        this.speed = speed;
        this.damage = damage;
        this.enemy = enemy;
        this.force = force;
    }

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (enemy == null)
            {
                Destroy(gameObject);
            }
            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.1)
            {
                enemy.TakeDamage(damage);
                //enemy.Knockback(100f);
                Destroy(gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, 0.1f * speed * Time.deltaTime);
        }
        catch (Exception)
        {
            Destroy(gameObject);
        }
        
    }
}
