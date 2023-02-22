using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class Pond : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 20; //20
    [SerializeField] private float slowerSpeed;
    public GameObject Player;
    [SerializeField] private CharacterMovement CM;
    void Start()
    {
        //CM = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player!= null){
            CM = FindObjectOfType<PlayerManager>().GetComponent<CharacterMovement>();
        }
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            CM.WalkSpeed = slowerSpeed;
            CM.ResetSpeed();
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            CM.WalkSpeed = initialSpeed;
            CM.ResetSpeed();
        }
    }
}
