using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyTemplate
{


    public override EnemyActions Behave(MapController map)
    {
        return EnemyActions.ADVANCE;
    }

    public Enemy1() : base(50, 10, 1, "Prefabs/enemy1")
    {

    }
}
