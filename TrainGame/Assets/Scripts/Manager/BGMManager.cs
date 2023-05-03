using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip startScreenClip, insideTrainClip, MappointClip;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    void Update()
    {
        
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        switch (next.name) {
            case "Tutorial":
                audioSource.Stop();
                audioSource.clip = MappointClip;
                audioSource.Play();
                break;
            case "StartScreen":
                audioSource.Stop();
                audioSource.clip = startScreenClip;
                audioSource.Play();
                break;
            case "MapPoint":
                audioSource.Stop();
                audioSource.clip = MappointClip;
                audioSource.Play();
                break;
            case "LeoPlayAround":
                audioSource.Stop();
                audioSource.clip = insideTrainClip;
                audioSource.Play();
                break;
        }
    }
}
