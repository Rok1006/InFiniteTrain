using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class RadiationManager : MonoBehaviour
{
    private int currentRadiationLevel = 0;
    [Foldout("Level 0"), Label("Enter State")] public UnityEvent enterRad0;
    [Foldout("Level 0"), Label("Update State")] public UnityEvent updateRad0;
    [Foldout("Level 0"), Label("Leave State")] public UnityEvent leaveRad0;
    [Foldout("Level 1"), Label("Enter State")] public UnityEvent enterRad1;
    [Foldout("Level 1"), Label("Update State")] public UnityEvent updateRad1;
    [Foldout("Level 1"), Label("Leave State")] public UnityEvent leaveRad1;
    [Foldout("Level 2"), Label("Enter State")] public UnityEvent enterRad2;
    [Foldout("Level 2"), Label("Update State")] public UnityEvent updateRad2;
    [Foldout("Level 2"), Label("Leave State")] public UnityEvent leaveRad2;


    //getters & setters
    public int CurrentRadiationLevel {get=>currentRadiationLevel; set=>currentRadiationLevel = value;}

    //FSM
    private RadiationStateBase currentState;
    public RadiationState0 radiationstate0 = new RadiationState0();
    public RadiationState1 radiationstate1 = new RadiationState1();
    public RadiationState2 radiationstate2 = new RadiationState2();

    public void ChangeState(RadiationStateBase newState)
    {
        if (currentState != newState) {
            if (currentState != null)
            {
                currentState.LeaveState(this);
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }
    }

    void Start()
    {
        currentState = radiationstate0;
    }

    void Update()
    {
        currentState.UpdateState(this);  
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeState(radiationstate1);
    }

    public void showRadiationLevel(string str) {
        Debug.Log(str);
    }
}
