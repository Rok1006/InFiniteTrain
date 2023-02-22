using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[CreateAssetMenu(fileName = "NewPointData", menuName = "Point/PointData")]
//This data script control the information of : Outdor points
//THis is for individual land pieces not a whole point
public class PointData : ScriptableObject
{
    [SerializeField, BoxGroup("Settings")] public bool isWetLand;
    [SerializeField, BoxGroup("Settings")] public bool havePond;
    [SerializeField, BoxGroup("Environment")] public int GrassAmt;
//--------------------
    //the prefab land frm resource folder shd have similar names of number/id specific; Start land End Land or middle land
    [SerializeField, BoxGroup("BaseSetting")] public string pointType;  //e.g WetLand1
    [Tooltip("The name shd Alligh with the land name in heiarchy")][SerializeField, BoxGroup("BaseSetting")] public string landType;   //Start land/End Land/Middle land; e.g MidLandA (Landtype+variationstype)
//--------------------
    [SerializeField, BoxGroup("TrapSetting")] public bool haveTraps;
    [SerializeField, BoxGroup("TrapSetting")] public int TrapAmt;
    //[SerializeField, BoxGroup("TrapSetting")] public string TrapType;
//--------------------
    [SerializeField, BoxGroup("EnemySetting")] public bool haveEnemy;
    [SerializeField, BoxGroup("EnemySetting")] public int NumberOfEnemy;
    [SerializeField, BoxGroup("EnemySetting")] public string EnemyType;
//--------------------
    [SerializeField, BoxGroup("ResourcesSetting")] public bool haveResources;
    [SerializeField, BoxGroup("ResourcesSetting")] public int resourceBoxNum;
    [SerializeField, BoxGroup("ResourcesSetting")] public string boxType; //box skin name; same as in spine or integer?


}
