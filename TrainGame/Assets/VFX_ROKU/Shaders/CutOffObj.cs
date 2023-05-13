using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class CutOffObj : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;
    public List<GameObject> enviroObject = new List<GameObject>();
    [ReadOnly]public GameObject currentObj;
    [ReadOnly]public GameObject previousObj;

    private void Awake(){
        
        mainCamera = GetComponent<Camera>();
    }

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, offset, out hit, offset.magnitude, wallMask)){
            currentObj = hit.transform.gameObject;
            if(currentObj!= previousObj){
                if(enviroObject.Count>0){
                    previousObj = enviroObject[0];
                }
                enviroObject.Add(hit.transform.gameObject);
                var j = enviroObject[0].transform.GetChild(0);
                j.GetComponent<Animator>().SetTrigger("behind");
            }
            
        }else{
            
            if(enviroObject.Count>0){
                var j = enviroObject[0].transform.GetChild(0);
                j.GetComponent<Animator>().SetTrigger("front");
                Invoke("Remove", .5f);
            }
            
        }
        
        if(enviroObject.Count>1){ //have things in list
            Debug.Log ("weeeee");
            var j = enviroObject[0].transform.GetChild(0);
            j.GetComponent<Animator>().SetTrigger("front");
            enviroObject.Remove(enviroObject[0]);
        }

        // for (int i = 0; i < hitObjects.Length; ++i){
        //     Material[] materials = hitObjects[i].transform.GetComponent<MeshRenderer>().materials;

        //     for (int m = 0; m < materials.Length; ++m){
        //     materials[m].SetVector("_CutoutPos", cutoutPos);
        //     materials[m].SetFloat("_CutoutSize", 0.1f);
        //     materials[m].SetFloat("_FalloffSize", .05f);
        //     }
        // }
    }
    void Remove(){
        enviroObject.Remove(enviroObject[0]);
        currentObj = null;
            previousObj = null;
    }
}
