using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyStats",menuName = "EnemyInfo/New Enemy")]
public class EnemyInfo : ScriptableObject
{
    public int Health = 1;
    public float Speed = 1;
    public Sprite Image;
}
