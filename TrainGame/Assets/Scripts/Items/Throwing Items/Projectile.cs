using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Projectile : MonoBehaviour
{
    [ReadOnly, BoxGroup("Info")] public GameObject destination;
    [ReadOnly, BoxGroup("Info")] public float timeToTake = 1.5f;
    private float currentTime = 0.0f;
    private bool isReachedDestination = false;
    public Vector3 start;
    private GameObject VFXObject;
    private Animator VFXObjectAnim;

    void Start()
    {
        start = this.transform.position;
        VFXObject = GameObject.Find("StunBlast#Electrify");
        if (VFXObject == null)
            Debug.Log("cant find VFXObject for " + name);
        else {
            VFXObject.SetActive(false);
            VFXObjectAnim = VFXObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        var center = (destination.transform.position + start) * 0.5f + new Vector3(0, -1, 0);
        Vector3 c1 = start - center;
        Vector3 c2 = destination.transform.position - center;
        if (currentTime < timeToTake)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Slerp(c1, c2, currentTime / timeToTake);
            transform.position += center;
        }
        else //reached position
        {
            VFXObject.transform.position = transform.position;
            VFXObject.SetActive(true);
            Debug.Log("reached position " + "\nused " + timeToTake + " time");
            StartCoroutine(WaitToDestroySelf());
        }
    }

    /*wait until all animation played,
      then destroy self*/
    IEnumerator WaitToDestroySelf() {
        while (VFXObjectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        VFXObject.transform.position = new Vector3(-100,-100,-100);
        Destroy(gameObject);
    }
}   