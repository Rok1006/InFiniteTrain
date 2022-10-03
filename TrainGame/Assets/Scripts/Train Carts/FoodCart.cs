using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class FoodCart : Cart
{
    private int foodCount = 0;

    //getters & setters
    public int FoodCount {get=>foodCount; set=>foodCount = value;}

    void Start()
    {
        CartRoom = GetComponent<Room>();
        Doors.AddRange(CartRoom.GetComponentsInChildren<Teleporter>());
        foreach (Teleporter teleporter in Doors)
            ConnectedRooms.Add(teleporter.TargetRoom);
        
    }

    
    void Update()
    {
        
    }
}
