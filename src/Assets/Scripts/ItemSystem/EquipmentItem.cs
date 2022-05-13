using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "InventorySystem/Items/Equipment")]
public class EquipmentItem : Item
{
    public int DefenseStat;
    private void Awake()
    {
        DefenseStat = 1;
    }
}
