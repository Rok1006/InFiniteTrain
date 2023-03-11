using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedLaserDrone : MonoBehaviour
{
    public Transform[] wayPoints;

    public enum State
    {
        PATROL,
        ATTACK,
        STOP

    }
    public LayerMask layermask;
    public int destPoint = 0;
    public float speed;
    private bool stop;
    private float chance = 0.4f;
    public State state;
    public GameObject player;
    public float range;
    public float fovAngle;
    private Rigidbody rb;
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
                //Detect();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.STOP:
                //Detect();
                break;


        }
    }

    void Move()
    {

        if (rb.velocity.x > 0)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
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
            this.state = State.ATTACK;
        }
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

}
