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
    private List<Vector2> path;
    //TODO: loot ta

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(EnemyTemplate template, List<Vector2> path)
    {
        this.template = template;
        this.hitpoints = template.hitpoints;
        this.speed = template.speed;
        this.bounty = template.bounty;
        this.path = path;
    }

    public void Advance()
    {
        if (path.Count == 0)
        {
            Debug.Log("Enemy is trying to advance, but its path is already over");
        }
        else
        {
            Vector2 target = path[0];
            Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.y);
            //Debug.Log("target position: " + targetPosition.ToString());
            //Debug.Log("move towards: " + Vector3.MoveTowards(transform.position, targetPosition, 0.1f).ToString());
            //Vector3 direction = targetPosition - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f * speed * Time.deltaTime);
        }
    }



    public void Behave(/*MapController.CellState grid, Vector2 targetNexus*/)
    {
        EnemyTemplate.EnemyActions action = template.Behave(/*grid, targetNexus*/);
        switch (action)
        {
            case EnemyTemplate.EnemyActions.ADVANCE:
                Advance();
                break;
        }
    }

    public Vector2 GetGridPosition()
    {
        return new Vector2((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.z));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (path.Count == 0)
        {
            return;
        }
        if (Math.Round(other.gameObject.transform.position.x) == path[0].x
            && Math.Round(other.gameObject.transform.position.z) == path[0].y) //TODO: this might be unreliable
        {
            Debug.Log(GetInstanceID() + " -> " + other.gameObject.name + "(" + other.gameObject.GetInstanceID() + ")");
            path.RemoveAt(0);
        }

    }
}
