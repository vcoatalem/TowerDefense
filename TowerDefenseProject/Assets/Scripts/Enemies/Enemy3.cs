using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyController
{

    public override EnemyActions Behave(/*MapController.CellState grid, Vector2 target*/)
    {
        Advance();
        return EnemyActions.ADVANCE;
    }

    public new void Awake()
    {
        base.hitpoints = 150;
        base.bounty = 50;
        this.speed = 5f;
        this.damage = 40;
        base.Awake();
    }
}
