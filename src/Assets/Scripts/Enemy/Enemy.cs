using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Melee,Ranged,Boss,Dead}
public enum EnemyState { Patrol, Agro, Retreating,Dead }
public abstract class Enemy
{

    public int health;

    public float speed;
    public float accuracy = 0.01f;
    public float AttackCoolDown = 0;
    //Instaniated in child classes
    protected EnemyType enemytype;
    public EnemyType EnemyType;

    protected EnemyState enemystate;
    public Enemy(int health, float speed,EnemyState state)
    {
        this.health = health;
        this.speed = speed;
        EnemyState = EnemyState.Patrol;
    }
public EnemyState EnemyState 
    { 
        get { return enemystate; }
        set { enemystate = value; }
    }

    protected virtual void Movement()
    {
        switch (EnemyState)
        {
            case EnemyState.Patrol:
                break;
            case EnemyState.Agro:
                break;
            case EnemyState.Retreating:
                break;
            default:
                break;
        }
    }
    public virtual void Attack()
    {

    }
    public virtual void Patrol()
    {

    }
    public virtual void Retreat()
    {

    }
}