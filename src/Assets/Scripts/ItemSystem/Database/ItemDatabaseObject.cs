using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item Database",menuName = "InventorySystem/Items/Database")]
public class ItemDatabaseObject : ScriptableObject,ISerializationCallbackReceiver
{
    public Item[] items;
    //public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();
    [ContextMenu("Update IDs")]
    public void UpdateId()
    {
        for (int i = 0; i <items.Length; i++)
        {
            if(items[i].Data.Id != i)
            {
                items[i].Data.Id = i;
                //GetItem.Add(i, items[i]);
            }
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateId();

    }

    public void OnBeforeSerialize()
    {
        //GetItem = new Dictionary<int, Item>();
    }
}
