using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;

public class SceneTransition : MonoBehaviour
{
    public MapManager mapManager;
    private GameObject door;
    [SerializeField] private string otherSceneName;

    private GoToLevelEntryPoint gotoLevelEntryPoint;

    private void Start()
    {
        door = this.gameObject;
        gotoLevelEntryPoint = GetComponent<GoToLevelEntryPoint>();
    }
    public void ToPlayerScene() {
        BetweenSceneManager.Instance.IsBackFromMapPoint = true;
        SceneManager.LoadScene("LeoPlayAround");
    }

    public void ToBattleScene()
    {
        Singleton.Instance.id = mapManager.player.GetComponent<Point>().id;
        door.SetActive(false);
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
        SceneManager.LoadScene("MapPoint");
    }

    public void saveAndToOtherScene(string otherScene) {
        if (otherScene != "LeoPlayAround")
        {
            Singleton.Instance.id = mapManager.player.GetComponent<Point>().id;
        }
        door.SetActive(false);
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
        // SceneManager.LoadScene(otherScene);
        gotoLevelEntryPoint.GoToNextLevel();
        MapManager.gameState = 0;
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            saveAndToOtherScene(otherSceneName);
    }
}
