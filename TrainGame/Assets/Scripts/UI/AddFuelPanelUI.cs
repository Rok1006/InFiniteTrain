using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.UI;


/*this is custom ui class for add fuel panel*/
public class AddFuelPanelUI : MonoBehaviour
{
    private List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] private List<Image> icons = new List<Image>();
    [SerializeField] private Sprite fuelSprite;
    [SerializeField] private List<string> inventoryNames; 
    private List<Inventory> inventories = new List<Inventory>();
    void Start()
    {
        slots.AddRange(GetComponentsInChildren<InventorySlot>());
        foreach (InventorySlot slot in slots)
            icons.Add(slot.GetComponentInChildren<Image>());

        foreach (string inventoryName in inventoryNames) {
            inventories.Add(Inventory.FindInventory(inventoryName, "Player1"));
        }
    }

    void Update()
    {
        int fuelCount = 0;
        foreach (Inventory inventory in inventories) {
            foreach (InventoryItem item in inventory.Content) {
                if (item != null)
                    if (item.ItemID.Equals("Fuel"))
                        fuelCount++;
            }
        }

        for (int i = 0; i < icons.Count; i++) {
            if (i < fuelCount) {
                icons[i].sprite = fuelSprite;
                icons[i].color = Color.white;
            } else {
                icons[i].sprite = null;
                icons[i].color = Color.clear;
            }
        }
    }

    public void AddFuel() {
        foreach (Inventory inventory in inventories) {
            foreach (InventoryItem item in inventory.Content) {
                if (item != null) {
                    if (item.ItemID.Equals("Fuel")) {
                        inventory.RemoveItemByID("Fuel", 1);
                        FindObjectOfType<FuelMachine>().AddFuel();
                        return;
                    }
                }
            }
        }
    }
}
