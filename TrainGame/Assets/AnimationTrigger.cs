using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class AnimationTrigger : MonoBehaviour
{
    public Animator theObj;
    [SerializeField] private MMF_Player collisionFeedback;

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            theObj.SetTrigger("Move");
            collisionFeedback.PlayFeedbacks();
        }
        
    }
}
