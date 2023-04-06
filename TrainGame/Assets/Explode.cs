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
    [SerializeField] private GameObject DetectSign;
    private Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<PlayerInformation>();
        ExplodePt = player.transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
        DetectSign.SetActive(false);
    }

    void Update()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
        if(Vector3.Distance(this.transform.position , player.transform.position  + new Vector3(0,1,0) ) < 3f){
            DetectSign.SetActive(false);
            playerInfo.CurrentRadiationValue++;
            GameObject e = Instantiate(particles, ExplodePt.transform.position, Quaternion.identity);  //need destroy the particle
            //e.transform.parent = this.transform;
            Destroy(this.gameObject);
        }

        if (rb.velocity.x > 0)
        {
            this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
            //DetectObj.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
           this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
           //DetectObj.transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }
}
