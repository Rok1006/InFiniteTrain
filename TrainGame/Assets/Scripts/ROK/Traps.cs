using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script shd be assigned on traps object 
public class Traps : MonoBehaviour
{
    public enum TrapType { NONE, MINE, SPIKE, DEADLYBOUND, SHARDSHOOTER }
    public TrapType currentType = TrapType.NONE;
    //public TrapType TT;

    [Header("Assignment")]
    Animator anim;
    [SerializeField] private int damage;
    [SerializeField] private float duration;
    [SerializeField] private float waitTime;
    [SerializeField] private List<GameObject> Effects = new List<GameObject>();
    [SerializeField] private List<GameObject> Projectile = new List<GameObject>();
    public bool inZone = false;

    void Start(){
        anim = this.gameObject.GetComponent<Animator>();
        Effects[0].SetActive(false);
        switch(currentType){
            case TrapType.MINE:

            break;
            case TrapType.SPIKE:
                StartCoroutine("Trap_Spike");
            break;
            case TrapType.DEADLYBOUND:

            break;
            case TrapType.SHARDSHOOTER:

            break;
        }

    }

    void Update(){
        switch(currentType){
            case TrapType.MINE:
                if(inZone){
                    Effects[0].SetActive(true);
                    anim.SetTrigger("explode");
                    inZone = false;
                    StartCoroutine("Trap_Mine");
                    //Effects[0].SetActive(false);
                    //destroy itself
                }
            break;
            case TrapType.SPIKE:
            break;
            case TrapType.DEADLYBOUND:

            break;
            case TrapType.SHARDSHOOTER:

            break;
        }
//-----test
        if(Input.GetKeyDown(KeyCode.K)){
            inZone = true;
        }
    }

    IEnumerator Trap_Mine(){
        Effects[0].SetActive(true);
        anim.SetTrigger("explode");
        inZone = false;
        yield return new WaitForSeconds(waitTime);
        Effects[0].SetActive(false);
    }
    IEnumerator Trap_Spike(){
        Effects[0].SetActive(false);
        Effects[0].SetActive(true);
        anim.SetTrigger("out");
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("in");
        yield return new WaitForSeconds(waitTime);
        anim.ResetTrigger("in");
        anim.ResetTrigger("out");
        Effects[0].SetActive(false);
        StartCoroutine("Trap_Spike");
    }
    private void Trap_DeadlyBound(){

    }
    private void Trap_ShardShooter(){

    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            inZone = true;
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            inZone = false;
        }
    }

}


