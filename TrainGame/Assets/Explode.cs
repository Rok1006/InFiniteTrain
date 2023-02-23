using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private RadiationManager rm;
    [SerializeField] private GameObject particles;
    private GameObject ExplodePt;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rm = GameObject.Find("GameManager").GetComponent<RadiationManager>();
        ExplodePt = player.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
        if(Vector3.Distance(this.transform.position , player.transform.position  + new Vector3(0,1,0) ) < 3f){
            rm.CurrentRadiationLevel++;
            GameObject e = Instantiate(particles, ExplodePt.transform.position, Quaternion.identity);  //need destroy the particle
            //e.transform.parent = this.transform;
            Destroy(this.gameObject);
        }
    }
}
