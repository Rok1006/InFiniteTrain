using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Tools;

public class FlameThrowerGuyPlus : EnemyBase
{
    public Transform[] wayPoints;
    public GameObject detect;
    public float fovAngle;
    public LayerMask layermask;
    public float range;
    public bool canAttack;
    public float attackduration = 3;


    private int destPoint = 0;
    private float chance = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        detect = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame


    public override void Move()
    {
        
        if(agent.destination.x > this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, -1);
        }
        else if(agent.destination.x < this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, 1);
        }

        if (wayPoints.Length == 0)
        {
            return;
        }
        //transform.position = Vector3.MoveTowards(transform.position, wayPoints[destPoint].position, speed);

        var direction = (wayPoints[destPoint].position - this.transform.position).normalized;
        var currentDestination = wayPoints[destPoint].position;

        if (Vector3.Distance(transform.position, currentDestination) < 1.5f)
        {
           
            Debug.Log("stoppp");
            destPoint = (destPoint + 1) % wayPoints.Length;
            var stopChance = Random.Range(0, 1f);
            if (stopChance < chance)
            {
                this.state = State.STOP;
                StartCoroutine(Stop());

            }
            anim.SetBool("Walking", false);
        }
        else
        {
             anim.SetBool("Walking", true);
             SmallDustEmit();
            agent.SetDestination(wayPoints[destPoint].position);
        }
    }
    public override void Detect()
    {

        //range = distance
        //angle = cone vision
         //thisAnim.SetBool("Walking", false);
        if (player != null)
        {
            
                //popUp = true;
                Vector3 dir = (player.transform.position + new Vector3(0, 5, 0) - transform.position).normalized;
                float angle = Vector3.Angle(dir, detect.transform.right * -1);
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
                            this.state = State.PREPARE;
                        }
                    }
                }

            
        }
    }
    public override void Attack()
    {
        agent.SetDestination(player.transform.position);
        attackduration -= Time.deltaTime;
        agent.speed = 3;
        // do shit

        if(attackduration < 0)
        {
            attackduration = 3;
            state = State.STOP;
            StartCoroutine(Stop());
        }
    }
    public override void MoveTowards()
    {
        if (agent.destination.x > this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, -1);
        }
        else if (agent.destination.x < this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, 1);
        }
        

        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            Debug.Log("ready to attack");
            anim.SetBool("Walking", false);
            state = State.ATTACK;
           
            
        }
        else
        {
            agent.SetDestination(player.transform.position);
            //anim.SetBool("Walking", true);
            //DustEmit();
        }
    }
}
