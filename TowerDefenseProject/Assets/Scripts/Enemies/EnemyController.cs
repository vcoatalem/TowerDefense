using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public enum EnemyTypes
    {
        ENEMY1
    }

    public enum EnemyActions
    {
        ADVANCE,
        ABILITY
    };


    protected int hitpoints;
    public int GetHitpoints => hitpoints;

    protected float speed;
    protected int bounty;
    protected int damage;
    protected List<Vector2> path;

    private HitpointsBarController hitpointsBar;

    // static members used for initialization purposes
    private static UnityEngine.Object hitpointsBarPrefab;
    private static Canvas canvas;

    public void SetPath(List<Vector2> path)
    {
        this.path = path;
    }

    //TODO: loot table


    //this is called by child Awakes
    public void Awake()
    {
        if (!EnemyController.hitpointsBarPrefab)
        {
            EnemyController.hitpointsBarPrefab = Resources.Load("Prefabs/HitpointsBar");
        }
        if (!canvas)
        {
            canvas = FindObjectOfType<Canvas>();
            Debug.Log("Canvas: " + canvas.ToString());
        }
        hitpointsBar = ((GameObject)Instantiate(hitpointsBarPrefab, canvas.transform)).GetComponent<HitpointsBarController>();
        hitpointsBar.Initialize(this);
    }

    /*
    public void Initialize(EnemyController template, List<Vector2> path)
    {
        this.hitpoints = template.hitpoints;
        this.speed = template.speed;
        this.bounty = template.bounty;
        this.damage = template.damage;
        this.path = path;
    }*/

    public void Advance()
    {
        if (path.Count == 0)
        {
            Debug.Log("Enemy is trying to advance, but its path is already over");
        }
        else
        {
            //Debug.Log(path.Count);
            Vector2 target = path[0];
            Vector3 targetPosition = new Vector3(target.x, transform.position.y, target.y);
            //Debug.Log("target position: " + targetPosition.ToString());
            //Debug.Log("move towards: " + Vector3.MoveTowards(transform.position, targetPosition, 0.1f).ToString());
            //Vector3 direction = targetPosition - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f * speed * Time.deltaTime);
        }
    }



    public virtual EnemyActions Behave(/*MapController.CellState grid, Vector2 targetNexus*/) { return EnemyActions.ADVANCE; }

    public void TakeDamage(int damage)
    {
        hitpoints -= damage;
        if (hitpoints < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            hitpointsBar.SetHealthBarValue(hitpoints);
        }
    }

    public void Knockback(float force)
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(path[0].x, transform.position.y, path[0].y) * - force, force * Time.deltaTime);
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
        int monsterPathLayer = 7;
        if (other.gameObject.layer == monsterPathLayer) //TODO: this might be unreliable
        {
            //Debug.Log(GetInstanceID() + " -> " + "reached position: " + transform.position.ToString() + "; " + (path.Count - 1) + " remaining");
            path.RemoveAt(0);
            if (path.Count == 0)
            {
                NexusController nexus = other.gameObject.GetComponent<NexusController>();
                nexus.TakeHit(damage);
                Destroy(gameObject);
            }
        }

    }
}
