using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    public MPTSceneManager MSM;
    public NavMeshAgent agent;
    public GameObject player;
    public Rigidbody rb;
    public Animator anim;
    public GameObject eletricObj;
    public GameObject DetectSign;
    public GameObject HitBox;
    public GameObject Indicator;

    [SerializeField] private GameObject dustPrefab; //the particle system: prefab
    [SerializeField] private GameObject emitPt; //the particle system: prefab
    [HideInInspector] public List<GameObject> dust = new List<GameObject>();
    public enum State
    {
        PATROL,
        PREPARE,
        ATTACK,
        STOP,
        STUN,
        GOBACK
    }

    public State state;
    // Start is called before the first frame update
    private void Awake()
    {
        MSM = GameObject.Find("MPTSceneManager").GetComponent<MPTSceneManager>();
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        anim = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        DetectSign.SetActive(false);
        eletricObj.SetActive(false);
        HitBox.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
       
        if (player == null)
        {
            player = GameObject.Find("PlayerMC");
        }

        switch (state)
        {
            case State.PATROL:
                Move();
                Detect();
                //popUp = false;
                break;
            case State.PREPARE:

                MoveTowards();

                break;
            case State.ATTACK:
                Attack();
                break;
            case State.STOP:
                Detect();
                break;
            case State.STUN:
               StartCoroutine(Stun());
                break;
            case State.GOBACK:
                //WalkBack();
                break;
        }
    }
    public virtual void Move()
    {
        
    }
    public virtual void Detect()
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void MoveTowards()
    {
        agent.isStopped = true;
        Debug.Log("gotta move");
    }

    public virtual IEnumerator Stop()
    {
        agent.isStopped = true;
        float duration = 3f; // 3 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        while (totalTime <= duration)
        {
            //rb.velocity = Vector3.zero;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* no need for now */
            yield return null;
        }

        agent.isStopped = false;
        this.state = State.PATROL;
        
        anim.SetBool("Walking", false);
    }

    public virtual IEnumerator Stun()
    {
        agent.isStopped = true;
        float duration = 6f; // 6 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        anim.SetBool("Walking", false);
        while (totalTime <= duration)
        {
            //rb.velocity = Vector3.zero;
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* no need for now */
            yield return null;
        }

        agent.isStopped = false;
        this.state = State.PATROL;
        
        anim.SetBool("Walking", true);
    }

    public void DustEmit()
    {
        if (Time.frameCount % 10 == 0 && emitPt != null)
        {
            GameObject d = Instantiate(dustPrefab, emitPt.transform.position, Quaternion.identity) as GameObject;
            dust.Add(d);
            Invoke("DestroyDust", .9f);
        }
    }
    public void SmallDustEmit()
    {
        if (Time.frameCount % 30 == 0 && emitPt != null)
        {
            GameObject d = Instantiate(dustPrefab, emitPt.transform.position, Quaternion.identity) as GameObject;
            dust.Add(d);
            Invoke("DestroyDust", .9f);
        }
    }
    private void DestroyDust()
    {
        Destroy(dust[0]);
        dust.RemoveAt(0);
    }
}
