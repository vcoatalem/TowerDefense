using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public Dictionary<EnemyTemplate.EnemyTypes, int> enemies { get; }
    public float spawnRate { get; }

    public Wave(Dictionary<EnemyTemplate.EnemyTypes, int> enemies, float spawnRate)
    {
        this.enemies = enemies;
        this.spawnRate = spawnRate;
    }

    public EnemyTemplate NextEnemy()
    {
        foreach (var keyValue in enemies)
        {
            EnemyTemplate.EnemyTypes type = keyValue.Key;
            if (enemies[type] > 0)
            {
                enemies[type] -= 1;
                switch (type)
                {
                    case EnemyTemplate.EnemyTypes.ENEMY1:
                        return new Enemy1();
                }
            }
        }
        return null;
    }

    public bool isOver()
    {
        return enemies.Values.Where(x => x != 0).Count() == 0;
    }
}
