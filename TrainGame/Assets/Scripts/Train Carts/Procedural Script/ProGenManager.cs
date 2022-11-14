using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class ProGenManager : MonoBehaviour
{
    [SerializeField] private GameObject trainCart;
    private List<Room> trainRooms = new List<Room>();
    public List<Cart> trainCarts = new List<Cart>();
    [SerializeField] private int trainLength;
    void Start()
    {
        SpawnTrainCarts(trainLength);
        setDoors(trainCarts);
    }

    
    void Update()
    {
        
    }

    public void SpawnTrainCarts(int length) {
        for (int i = 0; i < length; i++) {
            GameObject cart = Instantiate(trainCart, new Vector3(0,i*100,0), Quaternion.identity);
            trainRooms.Add(cart.GetComponentInChildren<Room>());
            trainRooms[trainRooms.Count-1].gameObject.AddComponent<Cart>();
            trainCarts.Add(cart.GetComponentInChildren<Cart>());
            
        }
    }

    public void setDoors(List<Cart> carts) {
        Debug.Log(carts.Count);
        for (int i = 0; i < carts.Count; i++) {
            Cart cart = carts[i];
            //left doors
            if (i > 0) {
                for (int j = 0; j < cart.LeftDoors.Count; j++) {
                    Teleporter leftDoor = cart.LeftDoors[j].GetComponent<Teleporter>();
                    leftDoor.Destination = carts[i-1].RightDoors[j].GetComponent<Teleporter>();
                    leftDoor.TargetRoom = carts[i-1].RightDoors[j].GetComponentInParent<Room>();
                }
            }

            //right doors
            if (i < carts.Count-1) {
                for (int j = 0; j < cart.RightDoors.Count; j++) {
                    Teleporter rightDoor = cart.RightDoors[j].GetComponent<Teleporter>();
                    rightDoor.Destination = carts[i+1].LeftDoors[j].GetComponent<Teleporter>();
                    rightDoor.TargetRoom = carts[i+1].LeftDoors[j].GetComponentInParent<Room>();
                }
            }
        }
    }
}
