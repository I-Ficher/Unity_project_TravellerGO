using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    
    Item item;
    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.instance.Remove(item);

        Destroy(gameObject);
    }
    public void AddItem(Item newitem)
    {
        item = newitem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Sphere:
                CaptureSceneManager.maxThrowAttepts += item.value;
                break;
            case Item.ItemType.Power:
                OverrideOrb.throwSpeed += item.value;
                break;
        }

        RemoveItem();
    }
}
