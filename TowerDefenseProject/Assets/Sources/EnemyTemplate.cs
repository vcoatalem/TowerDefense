using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTemplate
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

    public int hitpoints { get; }
    public float speed { get; }
    public int bounty { get; }

    public int damage { get; }

    public Object model { get; }

    public EnemyTemplate(int hitpoints, float speed, int bounty, int damage, string filepath)
    {
        this.hitpoints = hitpoints;
        this.speed = speed;
        this.bounty = bounty;
        this.damage = damage;
        model = Resources.Load(filepath);
    }

    public virtual EnemyActions Behave(/*MapController.CellState grid, Vector2 target*/) { return EnemyActions.ADVANCE; }
}
