using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;

/*this manager has access to all room components in the scene*/
public class RoomManager : MonoBehaviour
{
    [SerializeField, ReadOnly] private Car currentCar;
    [SerializeField, InfoBox("order the list from far right (train head) to far left", EInfoBoxType.Normal)] private List<Car> carList;

    //getters & setters
    public Car CurrentCar  {get=>currentCar;set=>currentCar=value;}
    public List<Car> CarList {get=>carList; set=>carList=value;}

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
