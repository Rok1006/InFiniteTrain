using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class Scene_Sound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    //[SerializeField, BoxGroup("MapRelated")] private AudioClip MapOpen, PutFlag, PlugFlag, Ba_JumpHit, Ba_GrdSmash;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySound(string clipName){
        string clipPath = "Audio/" + clipName; // Assuming "Audio" is the subfolder name
        AudioClip clip = Resources.Load<AudioClip>(clipPath);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
