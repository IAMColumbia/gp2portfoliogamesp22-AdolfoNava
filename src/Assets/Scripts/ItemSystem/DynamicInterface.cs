using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// This is for the UI that is the collection of items obtained in the game
/// </summary>
public class DynamicInterface : UI
{
    public GameObject inventoryPrefab;
    public int XStart;
    public int YStart;
    public int XSpaceBetweenItem;
    public int NumOfColumn;
    public int YSpaceBetweenItem;

    protected override void CreateDisplay()
    {
        ItemSlotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.InventoryItems.Length; i++)
        {
            var go = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<RectTransform>().localPosition = GetPosition(i);
            //Each of these method calls is for the varous interactions possible using the mouse and manipulating items in the inventory
            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnter(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExit(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnDragStart(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnDragEnd(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });
            inventory.Container.InventoryItems[i].slot = go;

            ItemSlotsOnInterface.Add(go, inventory.Container.InventoryItems[i]);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(XStart + (XSpaceBetweenItem * (i % NumOfColumn)), YStart + (-YSpaceBetweenItem * (i / NumOfColumn)), 0f);
    }
}
