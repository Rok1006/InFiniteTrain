using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class SceneTransition : MonoBehaviour
{
    public void ToPlayerScene() {
        SceneManager.LoadScene("LeoPlayAround");
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("MapPoint");
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            ToBattleScene();
    }
}
