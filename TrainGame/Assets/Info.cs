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
    [SerializeField, BoxGroup("Map")]private int playerTrainInterval;
    [SerializeField, BoxGroup("Map")]private int enemyTrainInterval;
    [SerializeField, BoxGroup("Map")]private int confirmedPlayerTrainLocal;
    //public GameObject CurrentSelectedPtObj;

    [SerializeField, BoxGroup("UI Info")] public bool IsViewingInventory = false;
    [BoxGroup("Save & Load")] public bool isNewGame = true;

    //getters & setters
    public int CurrentPlayerTrainInterval {get=>playerTrainInterval; set=>playerTrainInterval = value;}
    public int CurrentEnemyTrainInterval {get=>enemyTrainInterval; set=>enemyTrainInterval = value;}
    public int ConfirmedPlayerTrainLocal {get=>confirmedPlayerTrainLocal; set=>confirmedPlayerTrainLocal = value;}
}
