using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    //public Player player;
    //public MouseItem MouseItem = new MouseItem();
    public Inventory inventory;
    protected Dictionary<GameObject, InventorySlot> ItemSlotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < inventory.Container.InventoryItems.Length; i++)
        {
            inventory.Container.InventoryItems[i].parent = this;
        }
        CreateDisplay();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

    }

    protected abstract void CreateDisplay();

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> item in ItemSlotsOnInterface)
        {
            if (item.Value.Item.Id >= 0)
            {
                item.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = item.Value.ItemObject.UIObjectIcon;
                item.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                item.Key.GetComponentInChildren<TextMeshProUGUI>().text = item.Value.Amount == 1 ? "" : item.Value.Amount.ToString("");
            }
            else
            {
                item.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                item.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                item.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject go)
    {
        MouseData.SlotHoveredOver = go;
    }
    public void OnDrag(GameObject go)
    {
        if (MouseData.TempGameItemBeingDragged != null)
            MouseData.TempGameItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void OnDragEnd(GameObject go)
    {
        Destroy(MouseData.TempGameItemBeingDragged);
        if(MouseData.InterfaceMouseIsOver == null)
        {
            ItemSlotsOnInterface[go].RemoveItem();
            return;
        }
        if (MouseData.SlotHoveredOver)
        {
            InventorySlot MouseHoverSlotData = MouseData.InterfaceMouseIsOver.ItemSlotsOnInterface[MouseData.SlotHoveredOver];
            inventory.SwapItems(ItemSlotsOnInterface[go], MouseHoverSlotData);
            //inventory.MoveItem()
        }
    }

    public void OnDragStart(GameObject go)
    {

        MouseData.TempGameItemBeingDragged = CreateTempItem(go);

    }
    public GameObject CreateTempItem(GameObject go)
    {
        GameObject tempItem = null;
        if(ItemSlotsOnInterface[go].Item.Id >= 0)
        {
        tempItem = new GameObject();
        var rt = tempItem.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        tempItem.transform.SetParent(transform.parent);
        var img = tempItem.AddComponent<Image>();
        img.sprite = ItemSlotsOnInterface[go].ItemObject.UIObjectIcon;
        img.raycastTarget = false;
        }

        return tempItem;
    }
    public void OnExit(GameObject go)
    {
        MouseData.SlotHoveredOver = null;
    }
    public void OnExitInterface(GameObject go)
    {
        MouseData.InterfaceMouseIsOver = null;
    }
    public void OnEnterInterface(GameObject go)
    {
        MouseData.InterfaceMouseIsOver = go.GetComponent<UI>();
    }
}
    public static class MouseData
{
    public static UI InterfaceMouseIsOver;
    public static GameObject TempGameItemBeingDragged;
    public static GameObject SlotHoveredOver;
}

