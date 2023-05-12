using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.UI;

public class SideInventoryDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup displayCanvasGroup;
    [SerializeField] private InventoryDisplay inventoryDisplay;

    public CanvasGroup DisplayCanvasGroup {get=>displayCanvasGroup;}
    public InventoryDisplay InventoryDisplay {get=>inventoryDisplay;}

    public void TakeAll() {
        Inventory inventory = inventoryDisplay.TargetInventory;
        Inventory playerInventory = Inventory.FindInventory("BackpackInventory", "Player1");
        for (int i = 0; i < inventory.Content.Length; i++)
            if (playerInventory.NumberOfFreeSlots > 0)
                inventory.MoveItemToInventory(startIndex : i, Inventory.FindInventory("BackpackInventory", "Player1"));
    }
}
