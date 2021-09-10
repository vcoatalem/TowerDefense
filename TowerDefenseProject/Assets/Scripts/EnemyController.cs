using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private EnemyTemplate template;
    private int hitpoints;
    private float speed;
    private int bounty;
    //TODO: loot ta

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(EnemyTemplate template)
    {
        this.template = template;
        this.hitpoints = template.hitpoints;
        this.speed = template.speed;
        this.bounty = template.bounty;
    }

    public void Advance(Vector2 target)
    {
        Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.y);
        Vector3 direction = targetPosition - transform.position;
        transform.Translate(direction * speed);
    }

    public Vector2 GetGridPosition()
    {
        return new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.z));
    }
}
