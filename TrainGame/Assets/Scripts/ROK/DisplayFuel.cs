using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
//Attach this script on Display fuel object prefab
public class DisplayFuel : MonoBehaviour
{
    private bool move = false;
    public string targetObjName;
    [ReadOnly]public GameObject Target;
    [ReadOnly]public GameObject AppearPt;
    [ReadOnly]public ParticleSystem bling;
    public int speed;
    void Start()
    {
        Target = GameObject.Find(targetObjName);
        AppearPt = GameObject.Find("AppearPt");
        bling = GameObject.Find("Bling").GetComponent<ParticleSystem>();

        StartCoroutine("Insert");
    }

    void Update()
    {
        if(move){
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Target.transform.position, step);
        }
        if(this.transform.position == Target.transform.position&&move){
            bling.Play();
            move = false;
        }

    }
    IEnumerator Insert(){
        yield return new WaitForSeconds(1f);
        move = true;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
   }

}
