using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;

public class ResourceBox : MonoBehaviour
{
    private GameObject inventoryCanvas;
    [SerializeField] private string playerID, invnetoryName;
    private SideInventoryDisplay sideInventoryDisplay;
    private InventoryDisplay inventoryDisplay;
    void Start()
    {
        sideInventoryDisplay = FindObjectOfType<SideInventoryDisplay>();
        if (sideInventoryDisplay == null)
            Debug.LogWarning("Cannot find side inventory display");
        
        inventoryCanvas = sideInventoryDisplay.DisplayCanvasGroup;

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
            inventoryCanvas.SetActive(true);
        }
    }

    /// when enter trigger area
    /// set inventory display target's name to empty
    /// set inventory canvas to inactive
    void OnTriggerExit(Collider collider) {
        if (collider.tag.Equals("Player")) {
            inventoryDisplay.TargetInventoryName = "";
            inventoryCanvas.SetActive(false);
        }
    }
}
