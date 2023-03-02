using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
//INfo that get carry over to different scene and back
public class Info : MonoBehaviour
{
    public int pointID;
    public int CurrentSelectedPt;
    [SerializeField, BoxGroup("Map")]private int playerTrainInterval;
    [SerializeField, BoxGroup("Map")]private int enemyTrainInterval;

    //getters & setters
    public int CurrentPlayerTrainInterval {get=>playerTrainInterval; set=>playerTrainInterval = value;}
    public int CurrentEnemyTrainInterval {get=>enemyTrainInterval; set=>enemyTrainInterval = value;}
}
