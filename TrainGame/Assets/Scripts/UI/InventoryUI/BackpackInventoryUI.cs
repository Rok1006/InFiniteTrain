using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.InventoryEngine;

public class BackpackInventoryUI : MonoBehaviour
{
    [SerializeField] InventoryInputManager inventoryInput;
    void Start()
    {
        inventoryInput.ToggleInventory();
        Debug.Log("Open Inventory");
    }

    void Update()
    {
        
    }
}
