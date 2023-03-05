using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotPlus : InventorySlot, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltip;
    [SerializeField] private TextMeshProUGUI tooltipText;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        InventoryItem item = ParentInventoryDisplay.TargetInventory.Content[Index];
        if (item != null) {
            tooltip.SetActive(true);
            tooltipText.text = item.ShortDescription;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
