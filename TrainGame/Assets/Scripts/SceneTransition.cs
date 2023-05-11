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
        if(InfoSC.EnemyAppearState >=2&&!InfoSC.DeadCountDownStart){ //ever after boss appear, every go out enemy move forward
                InfoSC.CurrentEnemyTrainInterval += 1;
        }
        Debug.Log("hey boss move cus dead");

    }
    public void SimpleComeBack(){
        if(InfoSC.EnemyAppearState >=2&&!InfoSC.DeadCountDownStart){ //ever after boss appear, every go out enemy move forward
                InfoSC.CurrentEnemyTrainInterval += 1;
        }
    }
    // public void SimpleGetKicked(){
    //     if(InfoSC.EnemyAppearState >= 1){InfoSC.CurrentEnemyTrainInterval += 1;};
    // }

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
        if (Tutorial.instance.stepIndex == 1)
        {
            Tutorial.instance.stepIndex++;
        }
        if(Tutorial.instance.stepIndex == 6 && SceneManager.GetActiveScene().name == "MapPoint")
        {
            Tutorial.instance.stepIndex++;
        }
        if (other.GetComponent<PlayerManager>() != null) {
            if(InfoSC.ConfirmedSelectedPt >= 2 && InfoSC.EnemyAppearState < 1){
                InfoSC.EnemyAppearState = 1;
            }
            saveAndToOtherScene(otherSceneName);
            Info.Instance.isNewGame = false;
        }
    }
}
