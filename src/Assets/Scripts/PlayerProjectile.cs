using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int damage = 1;
    private void Start()
    {
        Destroy(this.gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<IEnemyMono>().LoseHealth(damage);
            Destroy(gameObject);
        }
    }
}
