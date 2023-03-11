using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MMSingleton<ApplicationManager>
{
    [SerializeField] private string _saveFolderName = "InventoryEngine";
    void Update() {
        // if (Input.GetKeyDown(KeyCode.Escape))
        //     Application.Quit();
        
        if (Input.GetKeyDown(KeyCode.O)) {
            MMSaveLoadManager.DeleteSaveFolder (_saveFolderName);
        }
    }

    public void NewGame() {
        MMSaveLoadManager.DeleteSaveFolder (_saveFolderName);
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
            Destroy(gameManager.gameObject);
        MMSceneLoadingManager.LoadScene ("LeoPlayAround");
    }

    public void ContinueGame() {
        MMSceneLoadingManager.LoadScene ("LeoPlayAround");
    }

    public void ExitGame() {
        Application.Quit();
    }

}
