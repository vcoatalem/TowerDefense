using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy
{

    public enum EnemyActions
    {
        ADVANCE,
        ABILITY
    };

    protected int hitpoints;
    protected float speed;
    protected int bounty;

    public virtual EnemyActions Behave(MapController map) { return EnemyActions.ADVANCE; }
}
