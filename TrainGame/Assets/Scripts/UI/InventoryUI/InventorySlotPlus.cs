using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotPlus : InventorySlot, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    void Update()
    {
        if (mouse_over)
        {
            Debug.Log("Mouse Over");
        }
    }
    public void OnMouseOver() {
        Debug.Log("Mouse is over GameObject.");
        if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index]))
            return;
        else {
            InventoryItem item = ParentInventoryDisplay.TargetInventory.Content[Index];
            Debug.Log(item.name);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
        base.OnPointerEnter(eventData);
        
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        mouse_over = false;
        Debug.Log("Mouse exit");
    }

    public void DebugLogSth() {
        Debug.Log("123");
    }

}
