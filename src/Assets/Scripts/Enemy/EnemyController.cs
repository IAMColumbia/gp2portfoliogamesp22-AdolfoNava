using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public IEnemyMono enemy;

    void Start()
    {
        enemy = GetComponent<IEnemyMono>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemy.Movement();
    }
}
