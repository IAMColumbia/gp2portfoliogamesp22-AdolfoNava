using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Object", menuName = "InventorySystem/Items/Recovery")]

public class RecoveryItem : Item
{
    public int recoveryValue;
    private void Awake()
    {
        recoveryValue = 1;
    }
}
