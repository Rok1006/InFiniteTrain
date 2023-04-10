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
    void OnEnable() {
        this.MMEventStartListening<MMInventoryEvent>();
    }

    void OnDisable() {
        this.MMEventStopListening<MMInventoryEvent>();
    }

    void Start() {
        
    }

    void Update() {
        if (!inventoryInput.InventoryIsOpen) {
            inventoryInput.OpenInventory();
        }
    }

    public virtual void OnMMEvent(MMInventoryEvent inventoryEvent) {
    }
}
