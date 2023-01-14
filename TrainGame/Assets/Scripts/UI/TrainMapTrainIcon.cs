using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*have referrence of things in Train Icon in Train Map*/
public class TrainMapTrainIcon : MonoBehaviour
{
    [SerializeField] private GameObject center;

    //getters & setters
    public GameObject Center {get=>center;set=>center=value;}
}
