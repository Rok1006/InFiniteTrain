using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] Inventory targetInven;

    void Start() {
        // MMEventManager.TriggerEvent(new MMGameEvent("Load"));
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        if (Input.GetKeyDown(KeyCode.O)) {
            // targetInven.ResetSavedInventory();
            // MMEventManager.TriggerEvent(new MMGameEvent("Save"));
            // MMEventManager.TriggerEvent(new MMGameEvent("Load"));
            string _saveFolderName = "InventoryEngine";
            MMSaveLoadManager.DeleteSaveFolder (_saveFolderName);
        }
    }
}
