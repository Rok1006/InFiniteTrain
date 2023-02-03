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
    [SerializeField] private List<GameObject> Objects = new List<GameObject>();
    [SerializeField] private List<GameObject> Point = new List<GameObject>();

    [SerializeField] private List<GameObject> Projectile = new List<GameObject>();
    public bool inZone = false;

    LineRenderer LR;

    void Start(){
        anim = this.gameObject.GetComponent<Animator>();

        if(Effects.Count>0){Effects[0].SetActive(false);}
        switch(currentType){
            case TrapType.MINE:
            break;
            case TrapType.SPIKE:
                StartCoroutine("Trap_Spike");
            break;
            case TrapType.DEADLYBOUND:
                LR = this.GetComponent<LineRenderer>();
                LR.positionCount = 2;
                StartCoroutine("Trap_DeadlyBound");
            break;
            case TrapType.SHARDSHOOTER:
                StartCoroutine("Trap_ShardShooter");
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
                LR.SetPosition(0, new Vector3(Point[0].transform.position.x, Point[0].transform.position.y, Point[0].transform.position.z));
                LR.SetPosition(1, new Vector3(Point[1].transform.position.x, Point[1].transform.position.y, Point[1].transform.position.z));
            break;
            case TrapType.SHARDSHOOTER:
                Debug.DrawRay(Point[0].transform.position, new Vector3(-40,0,0), Color.red);
                //StartCoroutine("ShardShoot");
                
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
    IEnumerator Trap_DeadlyBound(){
        LineRenderer lr = this.GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.enabled = true;
        yield return new WaitForSeconds(duration);
        lr.enabled = false;
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("Trap_DeadlyBound");
    }
    IEnumerator Trap_ShardShooter(){
        Projectile.Clear();
        Projectile.TrimExcess();
        Effects[0].SetActive(false);
        Effects[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        for(int i = 0; i<3;i++){
            GameObject s = Instantiate(Objects[0], Point[0].transform.position, Quaternion.identity);
            s.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("Trap_ShardShooter");
    }
    // IEnumerator ShardShoot(){
    //     yield return new WaitForSeconds(0);
    //     if(Projectile.Count>2){
    //         for(int i = 0; i<3;i++){
    //             Projectile[i].transform.Translate(Vector3.left * 100 * Time.deltaTime);
    //             yield return new WaitForSeconds(5f);
    //         }
    //     }
    //     yield return new WaitForSeconds(3f);
    // }
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


