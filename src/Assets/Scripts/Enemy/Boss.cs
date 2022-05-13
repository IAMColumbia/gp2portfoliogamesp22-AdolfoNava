using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    //public Inventory BossDrops;
    public Boss(int health, float speed, EnemyState state) : base(health, speed, state)
    {

        this.EnemyType = EnemyType.Boss;
    }
}
