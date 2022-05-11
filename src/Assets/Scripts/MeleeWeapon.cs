using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
            other.GetComponent<IEnemyMono>().LoseHealth(damage);
    }
}
