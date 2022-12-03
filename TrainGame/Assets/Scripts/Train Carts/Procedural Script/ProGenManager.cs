using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using NaughtyAttributes;

public class ProGenManager : MonoBehaviour
{
    [SerializeField] private GameObject trainCart;
    private List<Room> trainRooms = new List<Room>();
    public List<Cart> trainCarts = new List<Cart>();
    [SerializeField] private int trainLength;


    [SerializeField, BoxGroup("Enemy")]
    private GameObject enemy;

    public GameObject Enemy{get=>enemy;set=>enemy=value;}
    void Start()
    {
        SpawnTrainCarts(trainLength);
        setDoors(trainCarts);
        setGrounds(trainCarts);
    }

    
    void Update()
    {
        
    }

    public void SpawnTrainCarts(int length) {
        for (int i = 0; i < length; i++) {
            GameObject cart = Instantiate(trainCart, new Vector3(i*100,0,0), Quaternion.Euler(0, 90, 0));
            trainRooms.Add(cart.GetComponentInChildren<Room>());
            trainRooms[trainRooms.Count-1].gameObject.AddComponent<EnemyCart>();
            trainCarts.Add(cart.GetComponentInChildren<EnemyCart>());
        }
    }

    public void setDoors(List<Cart> carts) {
        for (int i = 0; i < carts.Count; i++) {
            Cart cart = carts[i];
            //left doors & target room
            if (i > 0) {
                for (int j = 0; j < cart.LeftDoors.Count; j++) {
                    Teleporter leftDoor = cart.LeftDoors[j].GetComponent<Teleporter>();
                    leftDoor.Destination = carts[i-1].RightDoors[j].GetComponent<Teleporter>();
                    leftDoor.TargetRoom = carts[i-1].RightDoors[j].GetComponentInParent<Room>();

                    //set up fader ID
                    leftDoor.FaderID = 1;
                }
            }

            //right doors & target room
            if (i < carts.Count-1) {
                for (int j = 0; j < cart.RightDoors.Count; j++) {
                    Teleporter rightDoor = cart.RightDoors[j].GetComponent<Teleporter>();
                    rightDoor.Destination = carts[i+1].LeftDoors[j].GetComponent<Teleporter>();
                    rightDoor.TargetRoom = carts[i+1].LeftDoors[j].GetComponentInParent<Room>();

                    //set up fader ID
                    rightDoor.FaderID = 1;
                }
            }
        }
    }

    public void setGrounds(List<Cart> carts) {
        foreach(Cart cart in carts)
            cart.setGround();
    }
}
