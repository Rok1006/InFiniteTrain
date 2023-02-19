using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class Pond : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 20; //20
    [SerializeField] private float slowerSpeed;
    [SerializeField] private CharacterMovement CM;
    void Start()
    {
        CM = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(gameObject.tag == "Trap"){
            CM.WalkSpeed = slowerSpeed;
            CM.ResetSpeed();
        }
    }
    private void OnTriggerExit(Collider col) {
        if(gameObject.tag == "Trap"){
            CM.WalkSpeed = initialSpeed;
            CM.ResetSpeed();
        }
    }
}
