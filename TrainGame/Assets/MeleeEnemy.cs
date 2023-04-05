using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Transform[] wayPoints;

    public enum State
    {
        PATROL,
        ATTACK,
        STOP,
        STUN
        
    }
    public GameObject player;
    public GameObject enemyChild;
    public LayerMask layermask;
    public int destPoint = 0;
    public float speed;
    public float approachSpeed;
    private bool stop;
    private float chance = 0.4f;
    public State state;
    public float range;
    public float fovAngle;
    private Rigidbody rb;
    bool canAttack = false;
    public GameObject DetectObj;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.PATROL:
                Move();
                Detect();
                
                break;
            case State.ATTACK:
                MoveTowards();
                if(canAttack == true)
                {
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
    }

    IEnumerator Stun()
    {
        float duration = 2f; // 2 seconds you can change this to
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
    void MoveTowards()
    {
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
        rb.velocity = dir * approachSpeed;

        if (Vector3.Distance(transform.position, player.transform.position) < 0.8f)
        {
            canAttack = true;
        }
    }
    void Move()
    {
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
        
        rb.velocity = direction * speed;

        var currentDestination = wayPoints[destPoint].position;

        if(Vector3.Distance(transform.position , currentDestination) < 0.8f)
        {
            destPoint = (destPoint + 1) % wayPoints.Length;
            var stopChance = Random.Range(0, 1f);
            if(stopChance < chance)
            {
                this.state = State.STOP;
                StartCoroutine(Stop());
                
            }
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
    }

    void Detect()
    {
        //range = distance
        //angle = cone vision
        if(player!=null){
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
