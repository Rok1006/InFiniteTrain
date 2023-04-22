using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using MoreMountains.TopDownEngine;
using NaughtyAttributes;

public class SceneTransition : MonoBehaviour
{
    [SerializeField, BoxGroup("REF")]private Info InfoSC;
    //[SerializeField, BoxGroup("REF")]private MapManager MM;
    public MapManager mapManager;
    private GameObject door;
    [SerializeField] private string otherSceneName;

    private GoToLevelEntryPoint gotoLevelEntryPoint;

    private void Start()
    {
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
        //MM = GameObject.FindGameObjectWithTag("Mehnager").GetComponent<MapManager>();
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
        //door.SetActive(false);
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
        SceneManager.LoadScene("MapPoint");
    }

    public void RespawnToPlayerScene() {
         //this shd trigger when player die once
        FindObjectOfType<RadiationManager>().DeathOfRadiation();
        MMSceneLoadingManager.LoadScene("LeoPlayAround");
    }
    public void SimpleGetKicked(){
        if(InfoSC.EnemyAppearState >= 1){InfoSC.CurrentEnemyTrainInterval += 1;};
    }

    public void saveAndToOtherScene(string otherScene) {
        if (otherScene != "LeoPlayAround")
        {
            Singleton.Instance.id = mapManager.player.GetComponent<Point>().id;
        }
        //door.SetActive(false);
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
        // SceneManager.LoadScene(otherScene);
        gotoLevelEntryPoint.GoToNextLevel();
        MapManager.gameState = 0;
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null) {
            if(InfoSC.ConfirmedSelectedPt == 3 && InfoSC.EnemyAppearState < 1){
                InfoSC.EnemyAppearState = 1;
            }
            saveAndToOtherScene(otherSceneName);
            Info.Instance.isNewGame = false;
        }
    }
}
