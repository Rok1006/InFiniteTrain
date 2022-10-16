using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*this is get the animator parameters from the top down engine and refence them to the MC back animator*/
public class AnimatorParameterReference : MonoBehaviour
{
    [SerializeField] private Animator backAnimator, frontAnimator;

    void Update()
    {
        backAnimator.SetBool("Dashing", frontAnimator.GetBool("Dashing"));
        backAnimator.SetFloat("xSpeed", frontAnimator.GetFloat("xSpeed"));
        backAnimator.SetFloat("HorizontalDirection", frontAnimator.GetFloat("HorizontalDirection"));
        backAnimator.SetFloat("zSpeed", frontAnimator.GetFloat("zSpeed"));
    }
}
