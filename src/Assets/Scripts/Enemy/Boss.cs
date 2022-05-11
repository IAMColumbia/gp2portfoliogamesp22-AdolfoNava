using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    //public Inventory BossDrops;
    public Boss(int health, float speed)
    {
        this.health = health;
        this.speed = speed;
        this.EnemyType = EnemyType.Boss;
        this.EnemyState = EnemyState.Patrol;
    }

    protected override void Movement()
    {
        base.Movement();
    }
    public override void Attack()
    {
        base.Attack();
        playerInfo.Health--;
        AttackCoolDown = 60;
    }
}
