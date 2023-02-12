using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerInformation : MonoBehaviour
{
    public Transform back;
    [ShowNonSerializedField, BoxGroup("Radiation")] private float currentRadiationValue = 0f; 
    [SerializeField, BoxGroup("Radiation")] private float maxRadiationValue = 10f, minRadiationValue = 0f;
    [SerializeField, BoxGroup("Fuel")]private int fuelAmt;

    //getters & setters
    public float CurrentRadiationValue {get=>currentRadiationValue; set=>currentRadiationValue=value;}
    public float MaxRadiationValue {get=>maxRadiationValue; set=>maxRadiationValue=value;}
    public float MinRadiationValue {get=>minRadiationValue; set=>minRadiationValue=value;}
    public int FuelAmt {get=>fuelAmt; set=>fuelAmt=value;}
}
