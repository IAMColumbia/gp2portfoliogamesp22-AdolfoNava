using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
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
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Health--;
            Destroy(gameObject);
        }
    }
}
