using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public GameObject player;
    public RadiationManager rm;
    public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rm = GameObject.Find("GameManager").GetComponent<RadiationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
        if(Vector3.Distance(this.transform.position , player.transform.position  + new Vector3(0,1,0) ) < 3f){
            rm.CurrentRadiationLevel++;
            Instantiate(particles, player.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
