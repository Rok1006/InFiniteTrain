using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCloudGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] cloudPrefab;
    [SerializeField] private GameObject cloudHolder;
    public List<GameObject> cloud = new List<GameObject>();
    [SerializeField] private GameObject destination;
    bool canEmit = false;

    [SerializeField] private int speed; //speed of cloud

    void Start()
    {
        
    }

    void Update()
    {
        //GenerateCloud();
        InvokeRepeating("GenerateCloud", 0, 5f);
        Move();
        if(cloudPrefab[0].transform.position == destination.transform.position){
            Invoke("DestoryCloud", .9f);
        }
    }
    private void Move(){
        float step = speed * Time.deltaTime;
        for (int i = 0; i<cloud.Count; i++){
            cloud[i].transform.position = Vector3.MoveTowards(cloud[i].transform.position, destination.transform.position, step);
        }
        

    }
    private void GenerateCloud(){
        canEmit = true;
        //if(Time.frameCount%10 == 0){
        int randomC = Random.Range(0,cloudPrefab.Length);
        var c = Instantiate(cloudPrefab[randomC], this.transform, false)as GameObject;
        c.transform.parent = cloudHolder.transform;
        cloud.Add(c);
        canEmit = false;
        //}

    }
    void DestoryCloud(){
        Destroy(cloud[0]);
        cloud.RemoveAt(0);
    }

}
