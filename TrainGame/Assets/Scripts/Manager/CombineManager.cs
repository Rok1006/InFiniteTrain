using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using NaughtyAttributes;

public class CombineManager : MonoBehaviour
{
    [SerializeField, ReadOnly] private InventoryItem itemA, itemB, itemCombination, currentSelectingItem;
    [SerializeField] private InventoryInputManager mainInvInputManager;

    //getters & setters
    public InventoryItem ItemA {get=>itemA; set=>itemA=value;}
    public InventoryItem ItemB {get=>itemB; set=>itemB=value;}
    

    void Update()
    {
        
    }

    /*combine itemA and itemB if they can
      return true if success, false otherwise*/
    public bool Combine() {
        if (itemA != null && itemB != null) {
            switch (itemA.ItemName.ToLower().Trim()) {
                case "carrot":
                    
                    break;
            }
        }
        return false;
    }
}
