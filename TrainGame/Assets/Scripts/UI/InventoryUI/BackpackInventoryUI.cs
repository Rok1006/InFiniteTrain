using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;

public class BackpackInventoryUI : MonoBehaviour, MMEventListener<MMInventoryEvent>
{
    [SerializeField] InventoryInputManager inventoryInput;
    [SerializeField] InventoryDisplay inventoryDisplay;
    [SerializeField] int firstSlotIndex = 0;

    //getters & setters
    public InventoryDisplay InventoryDisplay {get=>inventoryDisplay;}

    void OnEnable() {
        this.MMEventStartListening<MMInventoryEvent>();
    }

    void OnDisable() {
        this.MMEventStopListening<MMInventoryEvent>();
    }

    void Start() {
        Invoke("SelectFirstSlot", 0.1f);
    }

    void Update() {
        if (!inventoryInput.InventoryIsOpen) {
            inventoryInput.OpenInventory();
        }
    }

    public virtual void OnMMEvent(MMInventoryEvent inventoryEvent) {
    }

    public void SelectFirstSlot() {
        inventoryDisplay.SlotContainer[firstSlotIndex].Select();
    }
}
