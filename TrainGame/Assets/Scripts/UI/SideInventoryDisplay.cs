using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.UI;

public class SideInventoryDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup displayCanvasGroup;
    [SerializeField] private InventoryDisplay inventoryDisplay;

    public CanvasGroup DisplayCanvasGroup {get=>displayCanvasGroup;}
    public InventoryDisplay InventoryDisplay {get=>inventoryDisplay;}

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
