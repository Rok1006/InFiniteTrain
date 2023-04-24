using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator theObj;

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            theObj.SetTrigger("Move");
        }
        
    }
}
