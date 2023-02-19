using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] Inventory targetInven;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        if (Input.GetKeyDown(KeyCode.O)) {
            targetInven.ResetSavedInventory();
            // string _saveFolderName = "TrainGameSave";
            // MMSaveLoadManager.DeleteSaveFolder (_saveFolderName);
        }
    }
}
