using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
//attach this scrip on items that has specific ffunction 
public class ItemSpecificFuntion : MonoBehaviour
{
    [ReadOnly] public GameObject Player;
    public enum Item { NONE, EnemyDetector }
    public Item currentItem = Item.NONE;

    void Start()
    {
        switch(currentItem){
            case Item.EnemyDetector:
                //arrows appear within given time
            break;
        }
    }

    void Update()
    {
        
    }
}
