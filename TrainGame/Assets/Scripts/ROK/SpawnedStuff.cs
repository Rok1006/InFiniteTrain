using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//put this on all non character object, including spawned object/depth detect
//depthdetect between this and ither same type
public class SpawnedStuff : MonoBehaviour
{
    public GameObject OtherObject;
    public bool IsOverlapping = false;
    public float SphereCastRadius = 0.5f;
    public float SphereCastDistance = 1f;
    void Start()
    {
        IsOverlapping = false;
        //SphereCastRadius = this.transform.localScale / 2;
    }

    void Update()
    {
        if(OtherObject!=null){
            // if (Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity).Any(c => c.gameObject == OtherObject))
            // {
            //     IsOverlapping = true;
            // }
            // else
            // {
            //     IsOverlapping = false;
            // }
        Vector3 direction = OtherObject.transform.position - transform.position;
        RaycastHit hitInfo;

        if (Physics.SphereCast(transform.position, SphereCastRadius, direction, out hitInfo, SphereCastDistance))
        {
            if (hitInfo.collider.gameObject == OtherObject)
            {
                IsOverlapping = true;
            }
            else
            {
                IsOverlapping = false;
            }
        }
        else
        {
            IsOverlapping = false;
        }
        }
    }
    public void OnTriggerStay(Collider col) {
        // if(col.gameObject.layer == 26){ //MainObject:Box
        //     isOverlappWithMain=true;
        //     Debug.Log("Overlapped");
        // }
        if(col.gameObject.tag == "Trap"){
            // isOverlapped = true;
            // Debug.Log("overlapped");
        }
    }
}
