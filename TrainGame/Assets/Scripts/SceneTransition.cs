using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class SceneTransition : MonoBehaviour
{
    private GameObject door;
    [SerializeField] private string otherSceneName;

    private void Start()
    {
        door = this.gameObject;
    }
    public void ToPlayerScene() {
        SceneManager.LoadScene("LeoPlayAround");
    }

    public void ToBattleScene()
    {
        door.SetActive(false);
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
        SceneManager.LoadScene("MapPoint");
    }

    public void saveAndToOtherScene(string otherScene) {
        door.SetActive(false);
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
        SceneManager.LoadScene(otherScene);
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            saveAndToOtherScene(otherSceneName);
    }
}
