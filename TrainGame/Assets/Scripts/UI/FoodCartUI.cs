using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodCartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI foodCountText;
    private FoodCart foodCart;

    //getters & setters
    public FoodCart FoodCart{get=>foodCart;set=>foodCart=value;}
    void Start()
    {
        
    }

    
    void Update()
    {
        foodCountText.text = "Food Count : " + FoodCart.Foods.Count;
    }

    public void MoveToBackpack() {
        if (FoodCart != null) {
            FoodCart.MoveToBackpack();
        }
    }
}
