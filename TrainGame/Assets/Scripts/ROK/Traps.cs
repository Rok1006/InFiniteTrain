using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

//This script shd be assigned on traps object 
public class Traps : MonoBehaviour
{
    public enum TrapType { NONE, MINE, SPIKE, DEADLYBOUND, SHARDSHOOTER, DEADLYLASER }
    public TrapType currentType = TrapType.NONE;
    //public TrapType TT;

    [Header("Assignment")]
    Animator anim;
    public GameObject Player;
    [SerializeField] private float damage;
    [SerializeField] private float duration;
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    [SerializeField] private List<GameObject> Effects = new List<GameObject>();
    [SerializeField] private List<GameObject> Objects = new List<GameObject>();
    [SerializeField] private List<GameObject> Point = new List<GameObject>();

    [SerializeField] private List<GameObject> Projectile = new List<GameObject>();
    [SerializeField] private MMF_Player mmfPlayer;
    public bool inZone = false;

    LineRenderer LR;
    //EdgeCollider2D edgeCollider;

    private AudioSource audioSource;

    void Start(){
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if(Effects.Count>0){Effects[0].SetActive(false);}
        switch(currentType){
            case TrapType.MINE:
            break;
            case TrapType.SPIKE:
                StartCoroutine("Trap_Spike");
            break;
            case TrapType.DEADLYBOUND:
                this.GetComponent<BoxCollider>().enabled = false;
                LR = this.GetComponent<LineRenderer>();
                LR.positionCount = 2;
                StartCoroutine("Trap_DeadlyBound");
            break;
            case TrapType.SHARDSHOOTER:
                StartCoroutine("Trap_ShardShooter");
            break;
            case TrapType.DEADLYLASER:
                StartCoroutine("Trap_DeadlyLaser");
            break;
        }

    }

    void Update(){
        switch(currentType){
            case TrapType.MINE:
                if(inZone){
                    Effects[0].SetActive(true);
                    anim.SetTrigger("explode");
                    StartCoroutine("Trap_Mine");
                    Player.GetComponent<PlayerInformation>().CurrentRadiationValue += damage;
                    Debug.Log(Player.GetComponent<PlayerInformation>().CurrentRadiationValue);
                    inZone = false;
                    audioSource.Play();
                    MMFlashEvent.Trigger(Color.red, 0.3f, 0.6f, 0, 0, TimescaleModes.Unscaled);
                    Player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("Damage");
                    //Effects[0].SetActive(false);
                    //destroy itself
                }
            break;
            case TrapType.SPIKE:
                if(inZone){
                    Effects[0].SetActive(true);
                    anim.SetTrigger("Stun");
                    Player.GetComponent<PlayerInformation>().CurrentRadiationValue += damage;
                    Debug.Log(Player.GetComponent<PlayerInformation>().CurrentRadiationValue);
                    inZone = false;
                    audioSource.Play();
                    MMFlashEvent.Trigger(Color.red, 0.3f, 0.6f, 0, 0, TimescaleModes.Unscaled);
                    Player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("Damage");
                }
            break;
            case TrapType.DEADLYBOUND: //not using
                LR.SetPosition(0, new Vector3(Point[0].transform.position.x, Point[0].transform.position.y, Point[0].transform.position.z));
                LR.SetPosition(1, new Vector3(Point[1].transform.position.x, Point[1].transform.position.y, Point[1].transform.position.z));
                if(inZone){
                    Player.GetComponent<PlayerInformation>().CurrentRadiationValue += damage;
                    inZone = false;
                }
                // MeshCollider collider = GetComponent<MeshCollider>();
                // if(collider == null){
                //     collider = gameObject.AddComponent<MeshCollider>();
                // }
                // Mesh mesh = new Mesh();
                // LR.BakeMesh(mesh, true);
                // collider.sharedMesh = mesh;
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
        this.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(duration);
        lr.enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
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
            s.GetComponent<TrapProjectile>()._player = Player;
            s.GetComponent<TrapProjectile>()._damage = damage;
            s.GetComponent<TrapProjectile>()._speed = speed;
            s.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("Trap_ShardShooter");
    }
    IEnumerator Trap_DeadlyLaser(){
        //yield return new WaitForSeconds(waitTime);
        this.anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(duration);
        this.anim.SetTrigger("EndShoot");
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("Trap_DeadlyLaser");
    }
    private void OnTriggerEnter(Collider col) { //cant detect
        if(col.gameObject.tag == "Player"){
            inZone = true;
            Player = col.gameObject;
            Debug.Log("yes");
        }
        if(col.gameObject.tag == "Enemy")
        {
            col.GetComponent<GeneralEnemy>().speed = col.GetComponent<GeneralEnemy>().speed / 3;
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            inZone = false;
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.GetComponent<GeneralEnemy>().speed = col.GetComponent<GeneralEnemy>().speed * 3 ;
        }
    }

}
//Dumpser---------
//        // RaycastHit hit;
        // int layerMask = LayerMask.GetMask("Environment");
        // Vector3 rayStartPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        // if(Physics.BoxCast(rayStartPos,this.transform.localScale / 2.0f, Vector3.down, out hit, Quaternion.identity,  10, layerMask)){   //not detecting the tile but the ground
        //     Debug.DrawRay(rayStartPos, new Vector3(0,-10,0), Color.green);
        //     //Debug.Log("yep");
        //     Debug.Log("Object detected underneath: " + hit.collider.gameObject.name);
        // }else{
        //     Debug.DrawRay(rayStartPos, new Vector3(0,-10,0), Color.red);
        // }
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


