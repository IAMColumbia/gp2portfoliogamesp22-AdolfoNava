using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    public Ranged(int health, float speed)
    {
        this.health = health;
        this.speed = speed;
        this.EnemyType = EnemyType.Ranged;
        this.EnemyState = EnemyState.Patrol;
    }
}
