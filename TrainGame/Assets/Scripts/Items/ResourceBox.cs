using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class ResourceBox : MonoBehaviour
{
    private CanvasGroup inventoryCanvas;
    [SerializeField,BoxGroup("TDE")] private string playerID, invnetoryName;
    private SideInventoryDisplay sideInventoryDisplay;
    private InventoryDisplay inventoryDisplay;

    [SerializeField, BoxGroup("UI")] private Image radicalBar;
    [SerializeField, BoxGroup("UI")] private GameObject timer;
    [SerializeField, BoxGroup("Logic")] private bool isLocked = true;

    private bool isOpening = false, isPlayerNear = false;
    void Start()
    {
        sideInventoryDisplay = FindObjectOfType<SideInventoryDisplay>();
        if (sideInventoryDisplay == null)
            Debug.LogWarning("Cannot find side inventory display");
        
        inventoryCanvas = sideInventoryDisplay.DisplayCanvasGroup;
        inventoryDisplay = sideInventoryDisplay.InventoryDisplay;


        if (inventoryDisplay == null)
            Debug.LogWarning("Cannot find inventory display");
        
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;

    }

    
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space)) {
            isOpening = true;
        }


        if (isLocked) { //if player need to open the lock
            if (isOpening)
                radicalBar.fillAmount = Mathf.Min(radicalBar.fillAmount + 0.2f * Time.deltaTime, 1.0f);
            if (radicalBar.fillAmount >= 1 && isOpening) {
                inventoryDisplay.ChangeTargetInventory(invnetoryName);
                inventoryCanvas.alpha = 1;
                inventoryCanvas.interactable = true;
            }
        } else { //player are free to open it
            if (isOpening) {
                inventoryDisplay.ChangeTargetInventory(invnetoryName);
                inventoryCanvas.alpha = 1;
                inventoryCanvas.interactable = true;
            }
        }
    }

    /// when enter trigger area
    /// set inventory display target's name to this resrouce box's name
    /// set inventory canvas to active
    void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("Player")) {
            isPlayerNear = true;
            
            if (isLocked)
                timer.SetActive(true);
        }
    }

    /// when enter trigger area
    /// set inventory display target's name to empty
    /// set inventory canvas to inactive
    void OnTriggerExit(Collider collider) {
        if (collider.tag.Equals("Player")) {
            isPlayerNear = false;
            if (isLocked)
                timer.SetActive(false);
            isOpening = false;
            inventoryCanvas.alpha = 0;
            inventoryCanvas.interactable = false;
        }
    }
}
