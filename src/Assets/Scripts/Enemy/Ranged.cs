using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    public Ranged(int health, float speed, EnemyState state) : base(health, speed, state)
    {
        this.EnemyType = EnemyType.Ranged;
    }
}
