using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using NaughtyAttributes;

/*this class manage the items that player can pick, making sure there's only one item player is picking*/
public class ItemManager : MMSingleton<ItemManager>
{
    [ShowNonSerializedField] private ItemActivePicker item;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
