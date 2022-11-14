using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using System.Linq;

/*This is the parent class of train carts*/
public class Cart : MonoBehaviour
{
    private Room cartRoom; //the room this script is attached
    private List<Room> connectedRooms = new List<Room>(); //rooms that this room is connecting to
    [SerializeField] private List<Teleporter> doors = new List<Teleporter>(); //teleporters that in this room
    [SerializeField] private List<Teleporter> leftDoors = new List<Teleporter>(), rightDoors = new List<Teleporter>();
    [SerializeField] private string leftDoorName = "Teleporter Left", rightDoorName = "Teleporter Right";
    private Vector2Int[,] grid = new Vector2Int[10,3];
    [SerializeField] private Transform ground;

    //getters & setters
    public Room CartRoom {get=>cartRoom; protected set=>cartRoom = value;}
    public List<Room> ConnectedRooms {get=>connectedRooms; set=>connectedRooms = value;}
    public List<Teleporter> Doors {get=>doors; set=>doors = value;}
    public List<Teleporter> LeftDoors {get=>leftDoors; set=>leftDoors = value;}
    public List<Teleporter> RightDoors {get=>rightDoors; set=>rightDoors = value;}
    public Vector2Int[,] Grid {get=>grid; set=>grid = value;}

    public virtual void Awake() {
        CartRoom = GetComponent<Room>();
        Doors.AddRange(CartRoom.GetComponentsInChildren<Teleporter>());
        foreach (Transform leftDoor in FindChildren(this.transform, leftDoorName)) {
            LeftDoors.Add(leftDoor.GetComponent<Teleporter>());
        }
        foreach (Transform rightDoor in FindChildren(this.transform, rightDoorName)) {
            RightDoors.Add(rightDoor.GetComponent<Teleporter>());
            Debug.Log("setting right doors");
        }

        
    }

    /*search for reference that every train cart need*/
    public virtual void Start() {
        
    }

    /*triggers when player enter the room*/
    public virtual void EnterRoom() {
        Debug.Log("entered " + name);
    }

    /*triggers when player exit the room*/
    public virtual void ExitRoom() {
        Debug.Log("exited " + name);
    }

    public void setConnectedRooms() {
        foreach (Teleporter teleporter in LeftDoors)
            ConnectedRooms.Add(teleporter.TargetRoom);
        foreach (Teleporter teleporter in RightDoors)
            ConnectedRooms.Add(teleporter.TargetRoom);
        
        //delete self or redundant
        ConnectedRooms = ConnectedRooms.Distinct().ToList();
        List<int> deletingIndexes = new List<int>();
        for (int i = connectedRooms.Count-1; i >= 0; i--)
            if (ConnectedRooms[i].Equals(GetComponent<Room>()))
                deletingIndexes.Add(i);
        
        foreach(int deletingIndex in deletingIndexes)
            ConnectedRooms.RemoveAt(deletingIndex);
    }

    public void SplitIntoGrid() {

    }

    public Transform[] FindChildren(Transform transform, string name)
    {
        return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
    }
}
