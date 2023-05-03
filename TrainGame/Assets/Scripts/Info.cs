using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using MoreMountains.Tools;

//INfo that get carry over to different scene and back
public class Info : MMSingleton<Info>
{
    public int pointID;
    public int CurrentSelectedPt;
    public int ConfirmedSelectedPt;
    [SerializeField, BoxGroup("Map")]private int playerTrainInterval; //current/target interval
    [SerializeField, BoxGroup("Map")]private int confirmedPlayerTrainLocal; //saved interval
    [SerializeField, BoxGroup("Map")]private int enemyTrainInterval; //curremt/target interval
    [SerializeField, BoxGroup("Map")]private int confirmedEnemyTrainLocal; //saved interval

    [BoxGroup("State")]public int doorState; //0=close, 1=open
    [BoxGroup("State")]public int EnemyAppearState; //0=close, 1=open
    [BoxGroup("State")]public bool DeadCountDownStart; //0=close, 1=open
    [BoxGroup("State")]public float DeadTime; //first set time given, 120

    [BoxGroup("Radiation")]public float radAmt;
    [SerializeField, BoxGroup("FUEL")]private int fuelStorageAmt; //saved interval
    //public GameObject CurrentSelectedPtObj;

    [SerializeField, BoxGroup("UI Info")] public bool IsViewingInventory = false;
    [BoxGroup("Save & Load")] public bool isNewGame = true;

    //getters & setters
    public int CurrentPlayerTrainInterval {get=>playerTrainInterval; set=>playerTrainInterval = value;}
    public int ConfirmedPlayerTrainLocal {get=>confirmedPlayerTrainLocal; set=>confirmedPlayerTrainLocal = value;}
    public int CurrentEnemyTrainInterval {get=>enemyTrainInterval; set=>enemyTrainInterval = value;}
    public int ConfirmedEnemyTrainLocal {get=>confirmedEnemyTrainLocal; set=>confirmedEnemyTrainLocal = value;}
    public int FuelStorageAmt {get=>fuelStorageAmt; set=>fuelStorageAmt = value;}
}