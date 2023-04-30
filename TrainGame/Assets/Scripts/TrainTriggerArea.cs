using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

/*this class is used for map point scene, deciding whether players are in train's area or exploration area*/
public class TrainTriggerArea : MonoBehaviour
{
    [ReadOnly, SerializeField] private RadiationManager radiationManager;
    [ReadOnly, SerializeField] private PlayerManager player;
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
        if (!(SceneManager.GetActiveScene().name.Equals("Start Screen") || SceneManager.GetActiveScene().name.Equals("Start"))) {
            if (radiationManager == null)
                radiationManager = FindObjectOfType<RadiationManager>();
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("Player")) {
            radiationManager.IsRadiated = false;
            Debug.Log("Player Enter from train trigger area");
         
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag.Equals("Player")) {
            radiationManager.IsRadiated = true;
            Debug.Log("Player Exit from train trigger area");
        }
    }
}
