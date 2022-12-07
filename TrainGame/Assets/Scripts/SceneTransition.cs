using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class SceneTransition : MonoBehaviour
{
    public void ToBattleScene()
    {
        SceneManager.LoadScene("Map Test");
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerManager>() != null)
            ToBattleScene();
    }
}
