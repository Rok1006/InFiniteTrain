using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemy : MonoBehaviour
{
    public float speed;
    public Animator thisAnim;
    public GameObject eletricObj;
    public Rigidbody rb;
    public enum State
    {
        PATROL,
        ATTACK,
        STOP,
        STUN,
        GOBACK



    }
    public State state;


    public virtual IEnumerator Stun()
    {

        float duration = 2f; // 2 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        while (totalTime <= duration)
        {
            thisAnim.SetBool("Walking", false);
            eletricObj.SetActive(true);
            rb.velocity = Vector3.zero;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* no need for now */
            yield return null;
        }
        eletricObj.SetActive(false);
        this.state = State.PATROL;
    }

}
