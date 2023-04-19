using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : GeneralEnemy
{
    public Transform[] wayPoints;
    public ForceUpdate stun;
    public enum State
    {
        PATROL,
        ATTACK,
        STOP,
        STUN
        
    }
    public GameObject player;
    public GameObject enemyChild;
    public GameObject DetectObj;
    public Animator thisAnim;
    public LayerMask layermask;
    public int destPoint = 0;
    public float approachSpeed;
    private bool stop;
    private float chance = 0.4f;
    public State state;
    public float range;
    public float fovAngle;
    private Rigidbody rb;
    public bool canAttack = false;
    public GameObject DetectSign;
    public bool popUp = false;
    public GameObject eletricObj;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody>();
        DetectSign.SetActive(false);
        eletricObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.PATROL:
                Move();
                Detect();
                popUp = false;
                break;
            case State.ATTACK:
                MoveTowards();
                popUp = true;
                if(canAttack == true)
                {
                    thisAnim.SetBool("Walking", false);
                    thisAnim.SetTrigger("Attack");
                    //attack here and disable canAttack
                }
                if(Vector3.Distance(this.transform.position ,player.transform.position) > 20)
                {
                    this.state = State.PATROL;
                }
                break;
            case State.STOP:
                Detect();
                break;
            case State.STUN:
                StartCoroutine(Stun());
                break;     
        }
        if(popUp){
            DetectSign.SetActive(true);
            popUp = false;
        }else{
            DetectSign.SetActive(false);
        }
        if (stun.stun == true)
        {
            this.state = State.STUN;
            stun.stun = false;
        }
    }

    IEnumerator Stun()
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
    void MoveTowards() //Move towards enemy
    {
        //thisAnim.SetBool("Walking", true);
        if (rb.velocity.x > 0)
        {
            //enemyChild.transform.eulerAngles = new Vector3(0, 180, 0);
            this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
            DetectObj.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            //nemyChild.transform.eulerAngles = new Vector3(0, 0, 0);
           this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
           DetectObj.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        var dir = (player.transform.position - this.transform.position).normalized;
        

        if (Vector3.Distance(transform.position, player.transform.position) < .8f)
        {
            canAttack = true;
            this.state = State.STOP;
            StartCoroutine(Stop());
            thisAnim.SetBool("Walking", false);
        }else{
            rb.velocity = dir * approachSpeed;
            thisAnim.SetBool("Walking", true);
        }
    }
    void Move() //move frm waypt to waypt
    {
        //DetectSign.SetActive(false);
        Debug.Log(rb.velocity.x);
        if (rb.velocity.x > 0)
        {
            //enemyChild.transform.eulerAngles = new Vector3(0, 180, 0);
            this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
            DetectObj.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            //nemyChild.transform.eulerAngles = new Vector3(0, 0, 0);
           this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
           DetectObj.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        
        if (wayPoints.Length == 0)
        {
            return;
        }
        //transform.position = Vector3.MoveTowards(transform.position, wayPoints[destPoint].position, speed);
        
        var direction = (wayPoints[destPoint].position - this.transform.position).normalized;
        var currentDestination = wayPoints[destPoint].position;

        if(Vector3.Distance(transform.position , currentDestination) < .8f)
        {
            destPoint = (destPoint + 1) % wayPoints.Length;
            var stopChance = Random.Range(0, 1f);
            if(stopChance < chance)
            {
                this.state = State.STOP;
                StartCoroutine(Stop());
                
            }
            thisAnim.SetBool("Walking", false);
        }else{
            thisAnim.SetBool("Walking", true);
            rb.velocity = direction * speed;
        }

    }
    IEnumerator Stop()
    {
        float duration = 3f; // 3 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        while (totalTime <= duration)
        {
            rb.velocity = Vector3.zero;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* no need for now */
            yield return null;
        }

        this.state = State.PATROL;
        //thisAnim.SetBool("Walking", false);
    }

    void Detect()
    {
        //range = distance
        //angle = cone vision
       // thisAnim.SetBool("Walking", false);
        if(player!=null){
            //popUp = true;
            Vector3 dir = (player.transform.position + new Vector3(0 , 5 , 0) - transform.position).normalized;
            float angle = Vector3.Angle(dir, DetectObj.transform.forward);
            RaycastHit r;
        
            if(angle < fovAngle / 2)
            {
                
                Debug.DrawLine(transform.position, dir, Color.green);
                Debug.DrawRay(transform.position, dir);
                if (Physics.Raycast(transform.position, dir, out r, range, layermask))
                {
                    Debug.Log("df");

                    if (r.collider.gameObject != null)
                    {
                        Debug.Log(r.collider.gameObject.name);
                        this.state = State.ATTACK;
                    }
                }
            }   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ThrowItem")
        {
            this.state = State.STUN;
        }
    }
}
