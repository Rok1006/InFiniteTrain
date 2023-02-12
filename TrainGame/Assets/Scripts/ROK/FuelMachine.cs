using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelMachine : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Button FuelAdd_Button;
    [SerializeField] private Slider FuelBar;
    private int currentAmt;
    [SerializeField] private int addAmt; //temp

    [SerializeField] private Animator TrainWheel;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FuelAdd_Button.onClick.AddListener(AddFuel);
         //set a max
    }

    // Update is called once per frame
    void Update()
    {
        FuelBar.value = player.GetComponent<PlayerInformation>().FuelAmt;
    }
    public void AddFuel(){
        player.GetComponent<PlayerInformation>().FuelAmt += addAmt;
        TrainWheel.SetTrigger("pulse");
    }
}
