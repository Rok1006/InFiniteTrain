using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;

public class ResourceBox : MonoBehaviour
{
    private CanvasGroup inventoryCanvas;
    [SerializeField] private string playerID, invnetoryName;
    private SideInventoryDisplay sideInventoryDisplay;
    private InventoryDisplay inventoryDisplay;
    void Start()
    {
        sideInventoryDisplay = FindObjectOfType<SideInventoryDisplay>();
        if (sideInventoryDisplay == null)
            Debug.LogWarning("Cannot find side inventory display");
        
        if (inventoryCanvas != null)
            inventoryCanvas = sideInventoryDisplay.DisplayCanvasGroup;

        if (inventoryDisplay != null)
            inventoryDisplay = sideInventoryDisplay.InventoryDisplay;


        if (inventoryDisplay == null)
            Debug.LogWarning("Cannot find inventory display");
    }

    
    void Update()
    {
        
    }

    /// when enter trigger area
    /// set inventory display target's name to this resrouce box's name
    /// set inventory canvas to active
    void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("Player")) {
            inventoryDisplay.TargetInventoryName = invnetoryName;
            inventoryCanvas.alpha = 1;
        }
    }

    /// when enter trigger area
    /// set inventory display target's name to empty
    /// set inventory canvas to inactive
    void OnTriggerExit(Collider collider) {
        if (collider.tag.Equals("Player")) {
            inventoryDisplay.TargetInventoryName = "";
            inventoryCanvas.alpha = 0;
        }
    }
}
