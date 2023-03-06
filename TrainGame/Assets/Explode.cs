using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerInformation playerInfo;
    [SerializeField] private GameObject particles;
    private GameObject ExplodePt;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInformation>();
        ExplodePt = player.transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
        if(Vector3.Distance(this.transform.position , player.transform.position  + new Vector3(0,1,0) ) < 3f){
            playerInfo.CurrentRadiationValue++;
            GameObject e = Instantiate(particles, ExplodePt.transform.position, Quaternion.identity);  //need destroy the particle
            //e.transform.parent = this.transform;
            Destroy(this.gameObject);
        }
    }
}
