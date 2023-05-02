using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedLaserDrone : GeneralEnemy
{
    public Transform[] wayPoints;
    [SerializeField] private GameObject[] laserPt;
    [SerializeField] private GameObject LaserObj;
    [SerializeField] private Animator lazerDAnim;
    [SerializeField] private GameObject laserEffectSignal;



    public LayerMask layermask;
    public int destPoint = 0;
 
    private bool stop;
    private float chance = 0.4f;
 
    public GameObject player;
    public float range;
    public float fovAngle;
  
    public GameObject DetectSign;
    public bool popUp = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody>();
        LaserObj.SetActive(false);
        laserEffectSignal.SetActive(false);
        DetectSign.SetActive(false);
        eletricObj.SetActive(false);
        //lazerDAnim.SetTrigger("shoot");
    }

    void Update()
    {
        LaserObj.transform.position = laserPt[0].transform.position;
        switch (state)
        {
            case State.PATROL:
                Move();
                //Detect();
                popUp = false;
                break;
            case State.ATTACK:
                lazerDAnim.SetBool("moving", false);
                Attack();
                popUp = true;
                break;
            case State.STOP:
                lazerDAnim.SetBool("moving", false);
                //Detect();
                break;
                case State.STUN:
                StartCoroutine(Stun());
                break;


        }
        if (lazerDAnim.GetCurrentAnimatorStateInfo(0).IsName("PreShoot"))
        {
            laserEffectSignal.SetActive(true);
        }
        if (lazerDAnim.GetCurrentAnimatorStateInfo(0).IsName("LaserShoot"))
        {
            LaserObj.SetActive(true);
            laserEffectSignal.SetActive(false);
        }
        if (lazerDAnim.GetCurrentAnimatorStateInfo(0).IsName("LaserEnd"))
        {
            LaserObj.SetActive(false);
        }
        // if(popUp){
        //     DetectSign.SetActive(true);
        //     popUp = false;
        // }else{
        //     DetectSign.SetActive(false);
        // }

    }

    public override IEnumerator Stun()
    {
        float duration = 2f; // 2 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        while (totalTime <= duration)
        {
            eletricObj.SetActive(true);
            rb.velocity = Vector3.zero;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* no need for now */
            yield return null;
        }
        eletricObj.SetActive(false);
        this.state = State.PATROL;
    }

     

    void Move()
    {
        lazerDAnim.SetBool("moving", true);
        if (rb.velocity.x > 0)
        {
            this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (wayPoints.Length == 0)
        {
            return;
        }
        //transform.position = Vector3.MoveTowards(transform.position, wayPoints[destPoint].position, speed);
        var direction = (wayPoints[destPoint].position - this.transform.position).normalized;

        rb.velocity = direction * speed;

        var currentDestination = wayPoints[destPoint].position;

        if (Vector3.Distance(transform.position, currentDestination) < 0.8f)
        {
            destPoint = (destPoint + 1) % wayPoints.Length;
            var stopChance = Random.Range(0, 1f);
            if (stopChance < chance)
            {
                this.state = State.STOP;
                StartCoroutine(Stop());

            }
        }

    }

    void Attack()
    {
        Debug.Log("Df");
        this.rb.velocity = Vector3.zero;
        //do whatever u want here
        lazerDAnim.SetTrigger("shoot");
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //this.state = State.ATTACK;
            Attack();
            DetectSign.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            this.state = State.PATROL;
            DetectSign.SetActive(false);
        }
    }
    void Reset(){
        LaserObj.SetActive(false);
        laserEffectSignal.SetActive(false);
    }
    void Detect()
    {
        /*
        if (player != null)
        {
            Vector3 dir = (player.transform.position + new Vector3(0, 5, 0) - transform.position).normalized;
            float angle = Vector3.Angle(dir, transform.right);
            RaycastHit r;

            if (angle < fovAngle / 2)
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
        */
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ThrowItem")
        {
            this.state = State.STUN;
        }
    }

}
