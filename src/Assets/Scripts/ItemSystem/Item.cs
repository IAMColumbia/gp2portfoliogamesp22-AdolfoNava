using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Important,Equipment,Weapon,Recovery,Default
}
public enum Attributes
{ Health,Damage,Defense}

public abstract class Item : ScriptableObject
{
    public Sprite UIObjectIcon;
    public bool Stackable;
    public ItemType Type;
    [TextArea(15,20)]
    public string Description;
    public ItemContent Data = new ItemContent();
    public ItemContent CreateItem() 
    {
        ItemContent newItem = new ItemContent(this);
        return newItem;
    }


}
[Serializable]
public class ItemContent
{
    public string Name;
    public int Id =-1;
    public ItemBuff[] buffs;
    public ItemContent()
    {
        Name = "";
        Id = -1;
    }
    public ItemContent(Item item)
    {
        Name = item.name;
        Id = item.Data.Id;
        buffs = new ItemBuff[item.Data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
;
            buffs[i] = new ItemBuff(item.Data.buffs[i].value) { attribute = item.Data.buffs[i].attribute };
        }
    }


}
[Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public ItemBuff(int value)
    {
        this.value = value;
    }
}
