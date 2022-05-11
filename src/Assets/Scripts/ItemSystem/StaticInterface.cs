using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UI
{
    public GameObject[] slots;

    protected override void CreateDisplay()
    {
        ItemSlotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.InventoryItems.Length; i++)
        {
            var go = slots[i];


            AddEvent(go, EventTriggerType.PointerEnter, delegate { OnEnter(go); });
            AddEvent(go, EventTriggerType.PointerExit, delegate { OnExit(go); });
            AddEvent(go, EventTriggerType.BeginDrag, delegate { OnDragStart(go); });
            AddEvent(go, EventTriggerType.EndDrag, delegate { OnDragEnd(go); });
            AddEvent(go, EventTriggerType.Drag, delegate { OnDrag(go); });
            inventory.Container.InventoryItems[i].slot = go;
            ItemSlotsOnInterface.Add(go, inventory.Container.InventoryItems[i]);

        }
    }
}
