using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public GameObject shelf;
    public GameObject fuel;
    public GameObject desk;
    public static ItemHolder instance;
    public GameObject craft;
    public GameObject desk2;
    public GameObject shelf2;
    public GameObject mealButton;
    public GameObject[] tutorialArrow;
    public GameObject final;
    public GameObject text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
