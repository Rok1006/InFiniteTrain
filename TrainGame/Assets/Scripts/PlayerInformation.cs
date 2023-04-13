using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MoreMountains.InventoryEngine;
using UnityEngine.SceneManagement;

public class PlayerInformation : MonoBehaviour
{
    private Info InfoSC;
    public Transform back;
    [ReadOnly, SerializeField, BoxGroup("Radiation")] private float currentRadiationValue = 0f; 
    [SerializeField, BoxGroup("Radiation")] private float maxRadiationValue = 10f, minRadiationValue = 0f;
    [SerializeField, BoxGroup("Fuel")]private int fuelAmt;
    [SerializeField, BoxGroup("Inventory")] private string backpackName;
    [ShowNonSerializedField, BoxGroup("Inventory")] private Inventory backpackInven;
    [SerializeField, BoxGroup("Animation")] private Animator playerAnimator;
     

    //getters & setters
    public float CurrentRadiationValue {get=>currentRadiationValue; set=>currentRadiationValue=value;}
    public float MaxRadiationValue {get=>maxRadiationValue; set=>maxRadiationValue=value;}
    public float MinRadiationValue {get=>minRadiationValue; set=>minRadiationValue=value;}
    public int FuelAmt {get=>fuelAmt; set=>fuelAmt=value;}
    public Inventory BackpackInven {get=>backpackInven;set=>backpackInven=value;}
    public Animator PlayerAnimator {get=>playerAnimator;}

    void Start() {
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
        FuelAmt = InfoSC.FuelStorageAmt;
    }
    void Update() {
        if (!SceneManager.GetActiveScene().name.Equals("LoadingScene") && !SceneManager.GetActiveScene().name.Equals("Start Screen")) {
            if (backpackInven == null)
                backpackInven = GameObject.Find(backpackName).GetComponent<Inventory>();
        }
        InfoSC.FuelStorageAmt = FuelAmt;
    }
}
