using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    Animator doorAnim;
    AudioSource audioSource;
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            //doorAnim.SetTrigger("Open");
            //if (!audioSource.isPlaying)
                //audioSource.Play();
            
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            //doorAnim.SetTrigger("Close");
        }
    }
}
