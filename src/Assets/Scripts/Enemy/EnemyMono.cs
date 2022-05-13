using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMono
{
    void Movement();
    void Agro();
    void Patrol();
    void Attack();
    void Retreat();
    void LoseHealth(int damage);
}
public abstract class EnemyMono : MonoBehaviour, IEnemyMono
{
    public GameObject Player;
    public Player playerInfo;
    public GameObject Drop;
    public EnemyInfo enemyInfo;
    public int HealthData;
    public float SpeedData;
    public EnemyState State;
    protected SpriteRenderer SpriteHandler;

    public virtual void Awake()
    {
        SpriteHandler = GetComponent<SpriteRenderer>();
        SpriteHandler.sprite = enemyInfo.Image;
    }
    public abstract void Movement();
    public abstract void Agro();
    public abstract void Patrol();
    public abstract void Attack();
    public abstract void Retreat();
    public abstract void LoseHealth(int damage);
}
