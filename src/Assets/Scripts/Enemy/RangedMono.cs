using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMono : EnemyMono
{
    public Ranged Ranged;
    public GameObject ProjectileSpawner;
    public GameObject Projectile;
    public Vector2 ShootingDirection;
    public bool HitPlayer;
    SpriteRenderer ColorToSwitch;
    public float FireRate;
    public override void Awake()
    {
        Ranged = new Ranged(enemyInfo.Health, enemyInfo.Speed,EnemyState.Patrol);
        base.Awake();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = Player.GetComponent<Player>();
        State = Ranged.EnemyState;

    }    
    // Start is called before the first frame update
    void Start()
    {
        ColorToSwitch = GetComponent<SpriteRenderer>();
        HealthData = Ranged.health;
        SpeedData = Ranged.speed;

    }
    public override void Agro()
    {
        //Vector3.RotateTowards()
        Vector2 targetPos = Player.transform.position;

        ShootingDirection = targetPos - (Vector2)transform.position;
        RaycastHit2D rayinfo = Physics2D.Raycast(transform.position, ShootingDirection, 18f);
        if (rayinfo)
        {
            if (rayinfo.collider.gameObject.tag == "Player")
            {
                if (HitPlayer == false)
                {
                    HitPlayer = true;
                    ColorToSwitch.color = Color.red;
                }
            }
            else
            {
                if(Vector2.Distance(Player.transform.position, transform.position) > 23f)
                {
                    HitPlayer = false;
                    ColorToSwitch.color = Color.white;
                    State = Ranged.EnemyState = EnemyState.Patrol;
                    
                }
            }
        }
        if(HitPlayer == true)
        {
            ProjectileSpawner.transform.up = ShootingDirection;
            if (Time.time > Ranged.AttackCoolDown)
            {
                Ranged.AttackCoolDown = (Time.time + 1) / FireRate;
                    Attack();
            }
        }
    }

    public override void Attack()
    {
         GameObject ProjectObjectInScene = Instantiate(Projectile, ProjectileSpawner.transform.position, Quaternion.identity);
        ProjectObjectInScene.GetComponent<Rigidbody2D>().AddForce(ShootingDirection * Ranged.speed);
    }

    public override void Movement()
    {
        if (HealthData <= 0||Ranged.health<=0)
        {
            State=Ranged.EnemyState = EnemyState.Dead;
        }
        switch (Ranged.EnemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Agro:
                Agro();
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
        if (Vector2.Distance(gameObject.transform.position, Player.transform.position) < 10f)
        {
            Ranged.EnemyState = EnemyState.Agro;
            State = EnemyState.Agro;
             
        }
    }

    public override void Retreat()
    {

    }
    public override void LoseHealth(int damage)
    {
        HealthData -= damage;
        Ranged.health -= damage;
    }


}
