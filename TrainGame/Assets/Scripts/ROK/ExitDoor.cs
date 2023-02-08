using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    Animator doorAnim;
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            doorAnim.SetTrigger("Open");
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            doorAnim.SetTrigger("Close");
        }
    }
}
