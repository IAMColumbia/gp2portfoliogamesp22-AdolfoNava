using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    public Melee(int health,float speed)
    {
        this.health = health;
        this.speed = speed;
        this.EnemyType = EnemyType.Melee;
        this.EnemyState = EnemyState.Patrol;

    }
    public override void Attack()
    {
        base.Attack();
        playerInfo.Health--;
        AttackCoolDown = 60;
    }
}
