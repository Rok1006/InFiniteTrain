using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

public class SceneTransition : MonoBehaviour
{

    // public void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "Player")
    //     {
    //         SceneManager.LoadScene("Map Test");
    //     }
        
    // }
    public void ToBattleScene()
    {
        SceneManager.LoadScene("Map Test");
    }
}
