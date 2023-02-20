using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrone : MonoBehaviour
{
    public Transform[] wayPoints;

    public enum State
    {
        PATROL,
        ATTACK,
        STOP
        
    }
    private int destPoint = 0;
    public float speed;
    private bool stop;
    private float chance = 0.4f;
    public State state;
    public GameObject player;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
                break;
            case State.STOP:
                break;
            
                
        }
    }

    void Move()
    {
        if(wayPoints.Length == 0)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[destPoint].position, speed);

        var currentDestination = wayPoints[destPoint].position;

        if(Vector3.Distance(transform.position , currentDestination) < 0.5f)
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
            
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* no need for now */
            yield return null;
        }

        this.state = State.PATROL;
    }

    void Detect()
    {
        Vector3 dir = (transform.position - player.transform.position).normalized;
        float angle = Vector3.Angle(dir, transform.right);
        RaycastHit r;
        
        if(Physics.Raycast(transform.position, dir, out r, range))
        {
            Debug.Log("df");
            if(r.collider.gameObject != null)
            {
                Debug.Log(r.collider.gameObject.name);
            }
        }
        Debug.DrawRay(transform.position, dir * r.distance, Color.yellow);
    }

}
