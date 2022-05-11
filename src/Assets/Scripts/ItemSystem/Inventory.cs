using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]

public class Inventory : ScriptableObject
{
    public string FilePath;
    public ItemDatabaseObject Database;
    public InventorySet Container;
    public void OnEnable()
    {
#if UNITY_EDITOR
        Database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        Database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public bool AddItem(ItemContent item,int amount)
    {
        if (EmptySlotCount <= 0)
        {
            return false;
        }
        InventorySlot slot = FindItemOnInventory(item);
        if(!Database.items[item.Id].Stackable || slot == null)
        {
            SetEmptySlot(item, amount);
            return true;
        }
        slot.AddAmount(amount);
        return true;
        //if (item.buffs.Length > 0)
        //{
        //    SetEmptySlot(item, amount);
        //    return;
        //}
        //for (int i = 0; i < Container.InventoryItems.Length; i++)
        //{
        //    if (Container.InventoryItems[i].Item.Id == item.Id)
        //    {
        //        Container.InventoryItems[i].AddAmount(amount);
        //        return;
        //    }
        //}
        //SetEmptySlot(item, amount);

    }
    public int EmptySlotCount { get {
            int counter = 0;
            for (int i = 0; i < Container.InventoryItems.Length; i++)
            {
                if (Container.InventoryItems[i].Item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }
    public InventorySlot FindItemOnInventory(ItemContent item)
    {
        for (int i = 0; i < Container.InventoryItems.Length; i++)
        {
            if(Container.InventoryItems[i].Item.Id == item.Id)
            {
                return Container.InventoryItems[i];
            }
        }
        return null;
    }
    public InventorySlot SetEmptySlot(ItemContent item,int amount )
    {
        for (int i = 0; i < Container.InventoryItems.Length; i++)
        {
            if(Container.InventoryItems[i].Item.Id <= -1)
            {
                Container.InventoryItems[i].UpdateSlot(item, amount);
                return Container.InventoryItems[i];
            }
        }
        //Setup for full inventory
        return null;
    }
    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject)&&item1.CanPlaceInSlot(item2.ItemObject))
        {
            InventorySlot temp = new InventorySlot(item2.Item, item2.Amount);
            item2.UpdateSlot(item1.Item, item1.Amount);
            item1.UpdateSlot(temp.Item, temp.Amount);
        }
    }
    public void RemoveItem(ItemContent item)
    {
        for (int i = 0; i < Container.InventoryItems.Length; i++)
        {
            Container.InventoryItems[i].UpdateSlot(null, 0);
        }
    }
    [ContextMenu("Save")]
    public void Save()
    {
        //Json Approach
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath,FilePath));
        //bf.Serialize(file, saveData);
        //file.Close();
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, FilePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, FilePath)))
        {
            //Json Approach
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, FilePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, FilePath), FileMode.Open,FileAccess.Read);
            InventorySet newContainer = (InventorySet)formatter.Deserialize(stream);
            for (int i = 0; i < Container.InventoryItems.Length; i++)
            {
                Container.InventoryItems[i].UpdateSlot(newContainer.InventoryItems[i].Item, newContainer.InventoryItems[i].Amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }

}
[Serializable]public class InventorySet
{
    public InventorySlot[] InventoryItems = new InventorySlot[24];
    public void Clear()
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            InventoryItems[i].RemoveItem();
        }
    }
}
[Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [NonSerialized]
    public UI parent;
    [NonSerialized]
    public GameObject slot;
    public ItemContent Item;
    public int Amount;
    public Item ItemObject
    {
        get
        {
            if (Item.Id >= 0)
            {
                return parent.inventory.Database.items[Item.Id];
            }
            return null;
        }
    }
    public InventorySlot()
    {
        Item = new ItemContent();
        Amount = 0;
    }
    public void UpdateSlot(ItemContent item,int amount)
    {
        Item = item;
        Amount = amount;
    }
    public void RemoveItem()
    {
        Item = new ItemContent();
        Amount = 0;
    }
    public InventorySlot(ItemContent item,int amount)
    {
        Item = item;
        Amount = amount;
    }
    public void AddAmount(int ivalue)
    {
        Amount += ivalue;
    }
    public bool CanPlaceInSlot(Item item)
    {
        if (AllowedItems.Length <= 0 ||item ==null||item.Data.Id<0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (item.Type == AllowedItems[i])
                return true;
        }
        return false;
    }
}
