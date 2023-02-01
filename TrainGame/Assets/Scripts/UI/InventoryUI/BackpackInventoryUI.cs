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
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            MMInventoryEvent.Trigger(MMInventoryEventType.InventoryOpens, null, inventoryDisplay.TargetInventoryName, null, 0, 0, inventoryDisplay.PlayerID);
    }

    public void OnMMEvent(MMInventoryEvent inventoryEvent) {
        if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryCloseRequest)
        {
            inventoryInput.CloseInventory();
        }

        if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryOpens)
        {
            inventoryInput.OpenInventory();
        }
    }
}
