using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public Dictionary<EnemyController.EnemyTypes, int> enemies { get; }
    public float spawnRate { get; }

    public Wave(Dictionary<EnemyController.EnemyTypes, int> enemies, float spawnRate)
    {
        this.enemies = enemies;
        this.spawnRate = spawnRate;
    }

    public EnemyController.EnemyTypes NextEnemy()
    {
        foreach (var keyValue in enemies)
        {
            EnemyController.EnemyTypes type = keyValue.Key;
            if (enemies[type] > 0)
            {
                enemies[type] -= 1;
                return type;
            }
        }
        return EnemyController.EnemyTypes.ENEMY1;
    }

    public bool isOver()
    {
        return enemies.Values.Where(x => x != 0).Count() == 0;
    }
}
