using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Other : MonoBehaviour
{
    [SerializeField] private GameObject SoundObject;
    //public AudioSource[] footSteps;
    public List<AudioSource> footSteps = new List<AudioSource>();
    
    void Start()
    {
        AudioSource[] footSound = SoundObject.GetComponents<AudioSource>();
        //footSteps[0] = footSound[0];
        footSteps.Add(footSound[0]);
        footSteps.Add(footSound[1]);
        //footSteps[1] = footSound[1];
        //Debug.Log(footSound[0]==null);
    }

   
    void Update()
    {
        
    }
    public void PlayeFootStep(){
        int num = Random.Range(0,2);
        footSteps[num].Play();
        //Debug.Log("num=" + num);
    }
}
