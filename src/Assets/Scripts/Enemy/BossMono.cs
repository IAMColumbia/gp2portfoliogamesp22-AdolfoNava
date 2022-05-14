using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMono : EnemyMono
{
    public Boss Boss;
    public override void Awake()
    {
        Boss = new Boss(enemyInfo.Health, enemyInfo.Speed,EnemyState.Patrol);
        base.Awake();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = Player.GetComponent<Player>();
        State = Boss.EnemyState;
    }
    private void Start()
    {
        HealthData = Boss.health;
        SpeedData = Boss.speed;
    }
    private void FixedUpdate()
    {
        if (HealthData <= 0)
        {
            EnemyAudioManager.PlayDeathAudio(Boss.EnemyType.ToString());
            State = EnemyState.Dead;
            Destroy(gameObject);
        }
    }
    public override void Agro()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, SpeedData);
        Boss.AttackCoolDown--;
    }

    public override void Attack()
    {
        if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < 3.4f)
        {
            playerInfo.Health--;
        }
    }

    public override void Movement()
    {
        if (HealthData <= 0 || Boss.health <= 0)
        {
            State = Boss.EnemyState = EnemyState.Dead;
        }
        switch (Boss.EnemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Agro:
                Agro();
                if (Boss.AttackCoolDown <= 0)
                {
                    Attack();
                    Boss.AttackCoolDown = 30;
                }
                break;
            case EnemyState.Retreating:
                Retreat();
                break;
            case EnemyState.Dead:
                Instantiate(Drop, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    public override void Patrol()
    {
        //Only intended to start the fight after getting in range of the boss
        if (Vector2.Distance(gameObject.transform.position, Player.transform.position) < 30f)
        {
            Boss.EnemyState = EnemyState.Agro;
            State = EnemyState.Agro;
        }
    }
    public override void LoseHealth(int damage)
    {
        HealthData -= damage;
        Boss.health -= damage;
    }
    public override void Retreat()
    {

    }
}
