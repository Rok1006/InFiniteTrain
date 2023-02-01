using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class SceneTransition : MonoBehaviour
{
    private GameObject door;

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
        SceneManager.LoadScene("MapPoint");
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            ToBattleScene();
    }
}
