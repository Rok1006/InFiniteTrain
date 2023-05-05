using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using MoreMountains.InventoryEngine;

public class FuelMachine : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Button FuelAdd_Button;
    [SerializeField] private Slider FuelBar;
    private int currentAmt;
    [SerializeField] private int addAmt; //temp
    [SerializeField] private Animator TrainWheel;
    [SerializeField] private TextMeshProUGUI fuelNumDisplay;
    [Header("Display")]
    [SerializeField] private GameObject displayFuelPrefab;
    [SerializeField] private GameObject AppearPt;
    [SerializeField, Tooltip("add some fuel to inventory")] private int fuelAmt;
    [SerializeField] private InventoryItem fuel;

    [SerializeField, BoxGroup("Audio")] private AudioClip addFuelClip;
    [SerializeField, BoxGroup("Audio")] private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // FuelAdd_Button.onClick.AddListener(AddFuel);
        AppearPt= GameObject.Find("AppearPt");
         //set a max
    }

    // Update is called once per frame
    void Update()
    {
        FuelBar.value = player.GetComponent<PlayerInformation>().FuelAmt;
        fuelNumDisplay.text = player.GetComponent<PlayerInformation>().FuelAmt + "/ 10 MAX";
    }   

    /*add fuel only if there's fuel in fuel inventory*/
    public void AddFuel() {
        player.GetComponent<PlayerInformation>().FuelAmt += addAmt;
        TrainWheel.SetTrigger("pulse");
        GameObject f = Instantiate(displayFuelPrefab, AppearPt.transform.position, Quaternion.identity);  

        //play audio
        audioSource.PlayOneShot(addFuelClip);
    }
}