using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    public Melee(int health, float speed,EnemyState state) : base(health, speed,state)
    {
        this.EnemyType = EnemyType.Melee;
        this.EnemyState = EnemyState.Patrol;
    }
}
