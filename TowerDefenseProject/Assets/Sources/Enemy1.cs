using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyTemplate
{


    public override EnemyActions Behave(/*MapController.CellState grid, Vector2 target*/)
    {
        return EnemyActions.ADVANCE;
    }

    public Enemy1() : base(50, 10, 3, "Prefabs/enemy1")
    {

    }
}
