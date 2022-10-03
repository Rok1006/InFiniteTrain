using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

/*This is the parent class of train carts*/
public class Cart : MonoBehaviour
{
    private Room cartRoom; //the room this script is attached
    private List<Room> connectedRooms = new List<Room>(); //rooms that this room is connecting to
    private List<Teleporter> doors = new List<Teleporter>(); //teleporters that in this room

    //getters & setters
    public Room CartRoom {get=>cartRoom; protected set=>cartRoom = value;}
    public List<Room> ConnectedRooms {get=>connectedRooms; set=>connectedRooms = value;}
    public List<Teleporter> Doors {get=>doors; set=>doors = value;}

    /*triggers when player enter the room*/
    public virtual void EnterRoom() {
        Debug.Log("entered " + name);
    }

    /*triggers when player exit the room*/
    public virtual void ExitRoom() {
        Debug.Log("exited " + name);
    }
}
