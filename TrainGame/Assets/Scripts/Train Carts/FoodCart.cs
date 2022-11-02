using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

public class FoodCart : Cart
{
    private List<FoodItem> foods = new List<FoodItem>();
    [SerializeField] private FoodCartUI foodUI;

    //getters & setters
    public List<FoodItem> Foods {get=>foods; set=>foods = value;}

    public override void Start()
    {
        base.Start();
        // CartRoom = GetComponent<Room>();
        // Doors.AddRange(CartRoom.GetComponentsInChildren<Teleporter>());
        // foreach (Teleporter teleporter in Doors)
        //     ConnectedRooms.Add(teleporter.TargetRoom);
    }

    
    void Update()
    {
        
    }

    public override void EnterRoom()
    {
        base.EnterRoom();
        foodUI.gameObject.SetActive(true);
        foodUI.FoodCart = this;
    }

    public override void ExitRoom()
    {
        base.ExitRoom();
        foodUI.gameObject.SetActive(false);
    }

    /*move all stored items into backpack*/
    public void MoveToBackpack() {
        Inventory foodCartInven = GameObject.Find("FoodCartInventory").GetComponent<Inventory>();
        if (foodCartInven != null) {
            foreach(FoodItem food in Foods) {
                MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, food.TargetInventoryName, food, food.Quantity, 0, "Player1");
            }
            Foods.Clear();
        }
    }
}
