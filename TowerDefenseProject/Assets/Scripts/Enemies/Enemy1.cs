using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyController
{


    public override EnemyActions Behave(/*MapController.CellState grid, Vector2 target*/)
    {
        Advance();
        return EnemyActions.ADVANCE;
    }

    public void Awake()
    {
        base.Awake();
        base.hitpoints = 50;
        base.bounty = 10;
        this.speed = 10f;
        this.damage = 20;
    }
}
