using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*this class is used for map point scene, deciding whether players are in train's area or exploration area*/
public class TrainTriggerArea : MonoBehaviour
{
    private RadiationManager radiationManager;
    private PlayerManager player;
    void Start()
    {
        radiationManager = FindObjectOfType<RadiationManager>();
        if (radiationManager == null)
            Debug.Log("cant find radiation manager");

        player = FindObjectOfType<PlayerManager>();
        if (player == null) 
            Debug.Log("Cant find player");
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("Player")) {
            radiationManager.IsRadiated = false;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag.Equals("Player")) {
            radiationManager.IsRadiated = true;

            //equip weapon
            player.takeOutSword();
        }
    }
}
