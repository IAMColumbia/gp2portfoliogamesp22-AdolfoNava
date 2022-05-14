using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THe main way that the unity engine communicates to my enemy class system
public class MeleeMono : EnemyMono
{
    public Melee Melee;
    public GameObject[] PatrolPoints;
    public int CurrentPatrolPoint;

    public override void Awake()
    {
        Melee = new Melee(enemyInfo.Health,enemyInfo.Speed,EnemyState.Patrol);
        base.Awake();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = Player.GetComponent<Player>();
        State = Melee.EnemyState;

    }
    // Start is called before the first frame update
    void Start()
    {
        HealthData = Melee.health;
        SpeedData = Melee.speed;
    }


    public override void Movement()
    {
        if (Melee.health <= 0)
        {
            EnemyAudioManager.PlayDeathAudio(Melee.EnemyType.ToString());
            State =Melee.EnemyState = EnemyState.Dead;
        }   
        switch (Melee.EnemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Agro:                   
                Agro(); 
                if (Melee.AttackCoolDown <= 0)
                {
                    Attack();
                    Melee.AttackCoolDown = 30;
                }
                break;
            case EnemyState.Retreating:
                Retreat();
                break;
            case EnemyState.Dead:
                Instantiate(Drop, transform.position,Quaternion.identity);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }    
    public override void Patrol()
    {        
        if(Vector2.Distance(gameObject.transform.position, Player.transform.position) < 10f)
        {
            Melee.EnemyState = EnemyState.Agro;
            State = EnemyState.Agro;
        }
        if(CurrentPatrolPoint == PatrolPoints.Length)
        {
            CurrentPatrolPoint = 0;
        }
        if (Vector2.Distance(gameObject.transform.position, PatrolPoints[CurrentPatrolPoint].transform.position) < 1f)
        {
            if (CurrentPatrolPoint < PatrolPoints.Length)
                CurrentPatrolPoint++;
        }
        transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].transform.position, SpeedData);

    }
    public override void Agro()
    {
       transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, SpeedData);
        Melee.AttackCoolDown--;
    }
    public override void Attack()
    {
        if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) < 2)
        {
            playerInfo.Health--;
        }
    }
    public override void Retreat()
    {

    }
    public override void LoseHealth(int damage)
    {
        HealthData -= damage;
        Melee.health -= damage;
    }
}
