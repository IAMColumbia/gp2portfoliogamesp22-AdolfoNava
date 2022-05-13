using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour,ISerializationCallbackReceiver
{
    public Item item;

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        GetComponent<SpriteRenderer>().sprite = item.UIObjectIcon;
        UnityEditor.EditorUtility.SetDirty(GetComponent<SpriteRenderer>());
#endif
    }
}
