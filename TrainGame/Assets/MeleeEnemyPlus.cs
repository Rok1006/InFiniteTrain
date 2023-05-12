using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
public class MeleeEnemyPlus : EnemyBase
{
    public Transform[] wayPoints;
    public GameObject detect;
    public float fovAngle;
    public LayerMask layermask;
    public float range;
    public bool canAttack;
    public float attackduration = 1;


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

        if (agent.destination.x < this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, -1);
        }
        else if (agent.destination.x > this.transform.position.x)
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
            float angle = Vector3.Angle(dir, detect.transform.right * 1);
            RaycastHit r;

            if (angle < fovAngle / 2)
            {

                Debug.DrawLine(transform.position, dir, Color.green);
                Debug.DrawRay(transform.position, dir);
                if (Physics.Raycast(transform.position, dir, out r, range, layermask))
                {
                    

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
        if (agent.destination.x < this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, -1);
        }
        else if (agent.destination.x > this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, 1);
        }
        attackduration -= Time.deltaTime;
        agent.SetDestination(player.transform.position);
        anim.SetBool("Walking", false);
        anim.SetTrigger("Attack");
        MSM.SM.PlaySound("Slash_Light_4");
        HitBox.SetActive(true);
        if(attackduration < 0 && Vector3.Distance(player.transform.position , this.transform.position ) < 20f)
        {
            anim.SetBool("Walking", true);
            attackduration = 1;
            state = State.PREPARE;
        }else if(attackduration < 0 && Vector3.Distance(player.transform.position, this.transform.position) >= 20f)
        {
            anim.SetBool("Walking", true);
            attackduration = 1;
            state = State.PATROL;
            agent.speed = 5;
        }
    }
    public override void MoveTowards()
    {
        agent.speed = 12;
        if (agent.destination.x < this.transform.position.x)
        {
            detect.GetComponent<MMBillboard>().OffsetDirection = new Vector3(0, 0, -1);
        }
        else if (agent.destination.x > this.transform.position.x)
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
            anim.SetBool("Walking", true);
            DustEmit();
        }
    }
}