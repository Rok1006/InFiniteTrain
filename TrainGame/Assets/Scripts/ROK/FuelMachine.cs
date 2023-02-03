using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FuelMachine : MonoBehaviour
{
    [SerializeField] private Button FuelAdd_Button;
    [SerializeField] private Slider FuelBar;
    private int currentAmt;
    [SerializeField] private int addAmt; //temp

    [SerializeField] private Animator TrainWheel;

    void Start()
    {
         FuelAdd_Button.onClick.AddListener(AddFuel);
         //set a max
    }

    // Update is called once per frame
    void Update()
    {
        FuelBar.value = currentAmt;
    }
    public void AddFuel(){
        currentAmt += addAmt;
        TrainWheel.SetTrigger("pulse");
    }
}
