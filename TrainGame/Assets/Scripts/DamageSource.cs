using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MoreMountains.Feedbacks;
//This script is only on Laser drone laser and Deadly laser trap
public class DamageSource : MonoBehaviour
{
    private MPTSceneManager MSM;
    public float dmg;
    [ReadOnly]public GameObject Player;

    void Start()
    {
        MSM = GameObject.Find("MPTSceneManager").GetComponent<MPTSceneManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("shit");
            other.gameObject.GetComponent<PlayerInformation>().CurrentRadiationValue += this.dmg;
            Player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("Damage");
            Player.GetComponent<PlayerManager>().AttackBlast.SetActive(true);
            MMFlashEvent.Trigger(Color.red, 0.3f, 0.6f, 0, 0, TimescaleModes.Unscaled);
            MSM.SM.PlaySound("Injured");
        }
    }
}
