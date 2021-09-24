using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyController
{
    public override EnemyActions Behave(/*MapController.CellState grid, Vector2 target*/)
    {
        Advance();
        return EnemyActions.ADVANCE;
    }

    public new void Awake()
    {
        base.hitpoints = 25;
        base.bounty = 10;
        this.speed = 30f;
        this.damage = 25;
        base.Awake();
    }
}
