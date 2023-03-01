using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

/*the class execute before a lot of things, so set things up before spawning them*/
public class BetweenSceneManager : MMSingleton<BetweenSceneManager>
{
    [ShowNonSerializedField] private GameObject initialSpawnPoint;

    [ReadOnly] public bool IsBackFromMapPoint = false;
    [SerializeField, BoxGroup("Main Train")] private Vector3 mainTrainDoor;
    [SerializeField, BoxGroup("Main Train")] private string mainSceneName;
    void Start()
    {
        initialSpawnPoint = GameObject.Find("InitialSpawnPoint");
    }

    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("LoadingScreen")) {
            if (initialSpawnPoint == null)
                initialSpawnPoint = GameObject.Find("InitialSpawnPoint");
        }

        if (IsBackFromMapPoint && SceneManager.GetActiveScene().name.Equals(mainSceneName)) {
            SetSpawnPointBeforeSpawn(mainTrainDoor);
            IsBackFromMapPoint = false;
        }
    }

    public void SetSpawnPointBeforeSpawn(Vector3 point) {
        initialSpawnPoint.transform.position = point;
    }
}
