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
    [ShowNonSerializedField] private Inventory fuelInventory;
    [SerializeField, Tooltip("add some fuel to inventory")] private int fuelAmt;
    [SerializeField] private InventoryItem fuel;

    void Start()
    {
        fuelInventory = GameObject.Find("FuelInventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        FuelAdd_Button.onClick.AddListener(AddFuel);
        AppearPt= GameObject.Find("AppearPt");

        //add some fuel to fuel inventory
        fuelInventory.AddItem(fuel, fuelAmt);

         //set a max
    }

    // Update is called once per frame
    void Update()
    {
        FuelBar.value = player.GetComponent<PlayerInformation>().FuelAmt;
        fuelNumDisplay.text = player.GetComponent<PlayerInformation>().FuelAmt + "/ 50 MAX";

        if (Input.GetKeyDown(KeyCode.O))
            Debug.Log(fuelInventory.GetQuantity("Fuel"));
    }   

    /*add fuel only if there's fuel in fuel inventory*/
    public void AddFuel() {
        if (fuelInventory.GetQuantity("Fuel") >= addAmt) {
            Debug.Log("adding");
            player.GetComponent<PlayerInformation>().FuelAmt += addAmt;
            fuelInventory.RemoveItemByID("Fuel", addAmt);
            TrainWheel.SetTrigger("pulse");
            GameObject f = Instantiate(displayFuelPrefab, AppearPt.transform.position, Quaternion.identity);  
        }
    }
}
