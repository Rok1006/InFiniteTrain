using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using TMPro;

public class InventoryShowDetail : MonoBehaviour, MMEventListener<MMInventoryEvent>
{
    [SerializeField] private CanvasGroup nameTextBox;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private float YOffset;
    [SerializeField, Tooltip("time duration that text will appear on the screen")] private float maxAppearTime;
    private float currentAppearTime = 0.0f;
    [SerializeField, Tooltip("time to take the name text to lerp in/out")] private float maxLerpTime;
    private float currentLerpTime;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (currentAppearTime > 0.0f && currentAppearTime < maxAppearTime) {
            currentAppearTime += Time.deltaTime;
        } else if (currentAppearTime >= maxAppearTime) {
            lerpInOut(nameTextBox, 0, maxLerpTime);
        }

        if (currentLerpTime > 0.0f && currentLerpTime < maxLerpTime) {
            currentLerpTime += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening();
    }
    
    private void OnDisable()
    {
        this.MMEventStopListening();
    }

    public void OnMMEvent(MMInventoryEvent inventoryEvent)
    {
        Inventory targetInventory;
        switch (inventoryEvent.InventoryEventType) {
            case MMInventoryEventType.Select:
            targetInventory = inventoryEvent.Slot.ParentInventoryDisplay.TargetInventory;
            if (targetInventory.Content[inventoryEvent.Slot.Index] == null) return;

            //set up name text box
            nameTextBox.alpha = 1;
            nameText.text = targetInventory.Content[inventoryEvent.Slot.Index].ItemName;
            nameTextBox.transform.position = new Vector3(inventoryEvent.Slot.transform.position.x, 
                                                            inventoryEvent.Slot.transform.position.y + YOffset, 
                                                            inventoryEvent.Slot.transform.position.z);
            
            //start count down for the name text box to stay on screen
            currentLerpTime = 0.0f;
            currentAppearTime = 0.0f;
            currentLerpTime += Time.deltaTime;
            currentAppearTime += Time.deltaTime;
            break;
        }
        
    }

    public void lerpInOut(CanvasGroup obj, float targetNum, float timeToTake) {
        obj.alpha = Mathf.Lerp(obj.alpha, targetNum, currentLerpTime/timeToTake);
    }
}
