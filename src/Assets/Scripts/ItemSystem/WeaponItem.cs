using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public enum WeaponType
{
    Melee,Ranged
}
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "InventorySystem/Items/Weapon")]
class WeaponItem : Item
{
    public WeaponType WeaponType;
    public int DamageStat;
    private void Awake()
    {
        Type = ItemType.Weapon;
        WeaponType = WeaponType.Melee;
        DamageStat = 1;
    }
}

