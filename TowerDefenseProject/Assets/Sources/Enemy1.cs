using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    public override EnemyActions Behave(MapController map)
    {
        return EnemyActions.ADVANCE;
    }
}
